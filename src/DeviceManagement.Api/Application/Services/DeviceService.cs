using DeviceManagement.Api.Application.Interfaces;
using DeviceManagement.Api.Contracts.Requests;
using DeviceManagement.Api.Contracts.Responses;
using DeviceManagement.Api.Domain.Entities;
using DeviceManagement.Api.Domain.Enums;
using DeviceManagement.Api.Infrastructure.Repositories.Interfaces;

namespace DeviceManagement.Api.Application.Services;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;

    public DeviceService(IDeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }

    public async Task<DeviceResponse> CreateAsync(CreateDeviceRequest request)
    {
        var device = new Device(request.Name, request.Brand, request.State);

        var createdDevice = await _deviceRepository.AddAsync(device);

        return MapToResponse(createdDevice);
    }

    public async Task<DeviceResponse?> GetByIdAsync(Guid id)
    {
        var device = await _deviceRepository.GetByIdAsync(id);

        return device is null ? null : MapToResponse(device);
    }

    public async Task<IEnumerable<DeviceResponse>> GetAllAsync(string? brand, DeviceState? state)
    {
        var devices = await _deviceRepository.GetAllAsync(brand, state);

        return devices.Select(MapToResponse);
    }

    public async Task<DeviceResponse?> UpdateAsync(Guid id, UpdateDeviceRequest request)
    {
        var device = await _deviceRepository.GetByIdAsync(id);

        if (device is null)
        {
            return null;
        }

        device.Update(request.Name, request.Brand, request.State);

        await _deviceRepository.UpdateAsync(device);

        return MapToResponse(device);
    }

    public async Task<DeviceResponse?> PatchAsync(Guid id, PatchDeviceRequest request)
    {
        var device = await _deviceRepository.GetByIdAsync(id);

        if (device is null)
        {
            return null;
        }

        device.Patch(request.Name, request.Brand, request.State);

        await _deviceRepository.UpdateAsync(device);

        return MapToResponse(device);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var device = await _deviceRepository.GetByIdAsync(id);

        if (device is null)
        {
            return false;
        }

        device.EnsureCanBeDeleted();

        await _deviceRepository.DeleteAsync(device);

        return true;
    }

    private static DeviceResponse MapToResponse(Device device)
    {
        return new DeviceResponse
        {
            Id = device.Id,
            Name = device.Name,
            Brand = device.Brand,
            State = MapState(device.State),
            CreatedAt = device.CreatedAt
        };
    }

    private static string MapState(DeviceState state)
    {
        return state switch
        {
            DeviceState.Available => "available",
            DeviceState.InUse => "in-use",
            DeviceState.Inactive => "inactive",
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, "Unsupported device state.")
        };
    }
}