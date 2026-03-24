using DeviceManagement.Api.Contracts.Requests;
using DeviceManagement.Api.Contracts.Responses;
using DeviceManagement.Api.Domain.Enums;

namespace DeviceManagement.Api.Application.Interfaces;

public interface IDeviceService
{
    Task<DeviceResponse> CreateAsync(CreateDeviceRequest request);

    Task<DeviceResponse?> GetByIdAsync(Guid id);

    Task<IEnumerable<DeviceResponse>> GetAllAsync(string? brand, DeviceState? state);

    Task<DeviceResponse?> UpdateAsync(Guid id, UpdateDeviceRequest request);

    Task<DeviceResponse?> PatchAsync(Guid id, PatchDeviceRequest request);

    Task<bool> DeleteAsync(Guid id);
}