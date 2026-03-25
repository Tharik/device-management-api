using DeviceManagement.Api.Application.Services;
using DeviceManagement.Api.Contracts.Requests;
using DeviceManagement.Api.Domain.Entities;
using DeviceManagement.Api.Domain.Enums;
using DeviceManagement.Api.Domain.Exceptions;
using DeviceManagement.Api.Infrastructure.Repositories.Interfaces;
using FluentAssertions;
using Moq;

namespace DeviceManagement.Api.Tests.Application;

public class DeviceServiceTests
{
    private readonly Mock<IDeviceRepository> _deviceRepositoryMock;
    private readonly DeviceService _service;

    public DeviceServiceTests()
    {
        _deviceRepositoryMock = new Mock<IDeviceRepository>();
        _service = new DeviceService(_deviceRepositoryMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Null_When_Device_Does_Not_Exist()
    {
        _deviceRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Device?)null);

        var result = await _service.GetByIdAsync(Guid.NewGuid());

        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateAsync_Should_Create_Device_Successfully()
    {
        var request = new CreateDeviceRequest
        {
            Name = "iPhone 15",
            Brand = "Apple",
            State = DeviceState.Available
        };

        _deviceRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Device>()))
            .ReturnsAsync((Device d) => d);

        var result = await _service.CreateAsync(request);

        result.Name.Should().Be("iPhone 15");
        result.Brand.Should().Be("Apple");
        result.State.Should().Be("available");
    }

    [Fact]
    public async Task UpdateAsync_Should_Return_Null_When_Device_Does_Not_Exist()
    {
        _deviceRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Device?)null);

        var request = new UpdateDeviceRequest
        {
            Name = "Updated",
            Brand = "Brand",
            State = DeviceState.Available
        };

        var result = await _service.UpdateAsync(Guid.NewGuid(), request);

        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_Should_Throw_When_Changing_Name_Of_InUse_Device()
    {
        var device = new Device("iPhone 15", "Apple", DeviceState.InUse);

        _deviceRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(device);

        var request = new UpdateDeviceRequest
        {
            Name = "Galaxy S24",
            Brand = "Apple",
            State = DeviceState.InUse
        };

        var action = async () => await _service.UpdateAsync(device.Id, request);

        await action.Should().ThrowAsync<DeviceInUseModificationException>();
    }

    [Fact]
    public async Task DeleteAsync_Should_Return_False_When_Device_Does_Not_Exist()
    {
        _deviceRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Device?)null);

        var result = await _service.DeleteAsync(Guid.NewGuid());

        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_Should_Throw_When_Device_Is_InUse()
    {
        var device = new Device("iPhone 15", "Apple", DeviceState.InUse);

        _deviceRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(device);

        var action = async () => await _service.DeleteAsync(device.Id);

        await action.Should().ThrowAsync<DeviceInUseDeletionException>();
    }

    [Fact]
    public async Task PatchAsync_Should_Throw_When_Changing_Brand_Of_InUse_Device()
    {
        var device = new Device("iPhone 15", "Apple", DeviceState.InUse);

        _deviceRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(device);

        var request = new PatchDeviceRequest
        {
            Brand = "Samsung"
        };

        var action = async () => await _service.PatchAsync(device.Id, request);

        await action.Should().ThrowAsync<DeviceInUseModificationException>();
    }
}