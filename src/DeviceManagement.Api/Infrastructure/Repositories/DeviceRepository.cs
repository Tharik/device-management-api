using DeviceManagement.Api.Domain.Entities;
using DeviceManagement.Api.Domain.Enums;
using DeviceManagement.Api.Infrastructure.Repositories.Interfaces;

namespace DeviceManagement.Api.Infrastructure.Repositories;

public class DeviceRepository : IDeviceRepository
{
    private static readonly List<Device> Devices = new List<Device>();

    public Task<Device> AddAsync(Device device)
    {
        Devices.Add(device);
        return Task.FromResult(device);
    }

    public Task<Device?> GetByIdAsync(Guid id)
    {
        var device = Devices.FirstOrDefault(d => d.Id == id);
        return Task.FromResult(device);
    }

    public Task<IEnumerable<Device>> GetAllAsync(string? brand, DeviceState? state)
    {
        IEnumerable<Device> query = Devices;

        if (!string.IsNullOrWhiteSpace(brand))
        {
            query = query.Where(d => d.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase));
        }

        if (state.HasValue)
        {
            query = query.Where(d => d.State == state.Value);
        }

        return Task.FromResult(query);
    }

    public Task UpdateAsync(Device device)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Device device)
    {
        Devices.Remove(device);
        return Task.CompletedTask;
    }
}