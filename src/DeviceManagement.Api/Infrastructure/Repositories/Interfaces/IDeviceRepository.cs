using DeviceManagement.Api.Domain.Entities;
using DeviceManagement.Api.Domain.Enums;

namespace DeviceManagement.Api.Infrastructure.Repositories.Interfaces;

public interface IDeviceRepository
{
    Task<Device> AddAsync(Device device);

    Task<Device?> GetByIdAsync(Guid id);

    Task<IEnumerable<Device>> GetAllAsync(string? brand, DeviceState? state);

    Task UpdateAsync(Device device);

    Task DeleteAsync(Device device);
}