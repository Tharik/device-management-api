using DeviceManagement.Api.Domain.Entities;
using DeviceManagement.Api.Domain.Enums;
using DeviceManagement.Api.Domain.Exceptions;
using FluentAssertions;

namespace DeviceManagement.Api.Tests.Domain;

public class DeviceTests
{
    [Fact]
    public void Constructor_Should_Create_Device_With_Expected_Values()
    {
        var device = new Device("iPhone 15", "Apple", DeviceState.Available);

        device.Id.Should().NotBeEmpty();
        device.Name.Should().Be("iPhone 15");
        device.Brand.Should().Be("Apple");
        device.State.Should().Be(DeviceState.Available);
        device.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void Update_Should_Allow_Changing_Name_And_Brand_When_Device_Is_Not_InUse()
    {
        var device = new Device("iPhone 15", "Apple", DeviceState.Available);

        device.Update("Galaxy S24", "Samsung", DeviceState.Inactive);

        device.Name.Should().Be("Galaxy S24");
        device.Brand.Should().Be("Samsung");
        device.State.Should().Be(DeviceState.Inactive);
    }

    [Fact]
    public void Update_Should_Throw_When_Changing_Name_While_Device_Is_InUse()
    {
        var device = new Device("iPhone 15", "Apple", DeviceState.InUse);

        var action = () => device.Update("Galaxy S24", "Apple", DeviceState.InUse);

        action.Should().Throw<DeviceInUseModificationException>();
    }

    [Fact]
    public void Update_Should_Throw_When_Changing_Brand_While_Device_Is_InUse()
    {
        var device = new Device("iPhone 15", "Apple", DeviceState.InUse);

        var action = () => device.Update("iPhone 15", "Samsung", DeviceState.InUse);

        action.Should().Throw<DeviceInUseModificationException>();
    }

    [Fact]
    public void Update_Should_Allow_Changing_Only_State_When_Device_Is_InUse()
    {
        var device = new Device("iPhone 15", "Apple", DeviceState.InUse);

        device.Update("iPhone 15", "Apple", DeviceState.Inactive);

        device.Name.Should().Be("iPhone 15");
        device.Brand.Should().Be("Apple");
        device.State.Should().Be(DeviceState.Inactive);
    }

    [Fact]
    public void Patch_Should_Throw_When_Changing_Name_While_Device_Is_InUse()
    {
        var device = new Device("iPhone 15", "Apple", DeviceState.InUse);

        var action = () => device.Patch("Galaxy S24", null, null);

        action.Should().Throw<DeviceInUseModificationException>();
    }

    [Fact]
    public void EnsureCanBeDeleted_Should_Throw_When_Device_Is_InUse()
    {
        var device = new Device("iPhone 15", "Apple", DeviceState.InUse);

        var action = () => device.EnsureCanBeDeleted();

        action.Should().Throw<DeviceInUseDeletionException>();
    }

    [Fact]
    public void EnsureCanBeDeleted_Should_Not_Throw_When_Device_Is_Not_InUse()
    {
        var device = new Device("iPhone 15", "Apple", DeviceState.Available);

        var action = () => device.EnsureCanBeDeleted();

        action.Should().NotThrow();
    }
}