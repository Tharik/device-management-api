using DeviceManagement.Api.Domain.Entities;
using DeviceManagement.Api.Domain.Enums;
using DeviceManagement.Api.Infrastructure.Persistence;
using DeviceManagement.Api.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement.Api.Infrastructure.Repositories;

public class DeviceRepository : IDeviceRepository
{
    private readonly AppDbContext _dbContext;

    public DeviceRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Device> AddAsync(Device device)
    {
        await _dbContext.Devices.AddAsync(device);
        await _dbContext.SaveChangesAsync();

        return device;
    }

    public async Task<Device?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Devices.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<Device>> GetAllAsync(string? brand, DeviceState? state)
    {
        IQueryable<Device> query = _dbContext.Devices.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(brand))
        {
            query = query.Where(d => d.Brand == brand);
        }

        if (state.HasValue)
        {
            query = query.Where(d => d.State == state.Value);
        }

        return await query.ToListAsync();
    }

    public async Task UpdateAsync(Device device)
    {
        _dbContext.Devices.Update(device);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Device device)
    {
        _dbContext.Devices.Remove(device);
        await _dbContext.SaveChangesAsync();
    }
}