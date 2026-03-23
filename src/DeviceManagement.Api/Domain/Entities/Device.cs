using DeviceManagement.Api.Domain.Enums;
using DeviceManagement.Api.Domain.Exceptions;

namespace DeviceManagement.Api.Domain.Entities;

public class Device
{
    private Device()
    {
    }

    public Device(string name, string brand, DeviceState state)
    {
        Id = Guid.NewGuid();
        Name = name;
        Brand = brand;
        State = state;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Brand { get; private set; } = string.Empty;

    public DeviceState State { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public void Update(string name, string brand, DeviceState state)
    {
        ValidateNameAndBrandModification(name, brand);

        Name = name;
        Brand = brand;
        State = state;
    }

    public void Patch(string? name, string? brand, DeviceState? state)
    {
        ValidateNameAndBrandModification(name, brand);

        if (name is not null)
        {
            Name = name;
        }

        if (brand is not null)
        {
            Brand = brand;
        }

        if (state.HasValue)
        {
            State = state.Value;
        }
    }

    public void EnsureCanBeDeleted()
    {
        if (State == DeviceState.InUse)
        {
            throw new DeviceInUseDeletionException();
        }
    }

    private void ValidateNameAndBrandModification(string? newName, string? newBrand)
    {
        if (State != DeviceState.InUse)
        {
            return;
        }

        var isChangingName =
            newName is not null &&
            !string.Equals(Name, newName, StringComparison.Ordinal);

        var isChangingBrand =
            newBrand is not null &&
            !string.Equals(Brand, newBrand, StringComparison.Ordinal);

        if (isChangingName || isChangingBrand)
        {
            throw new DeviceInUseModificationException();
        }
    }
}