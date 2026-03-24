using DeviceManagement.Api.Domain.Enums;

namespace DeviceManagement.Api.Contracts.Requests;

public class CreateDeviceRequest
{
    public string Name { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;

    public DeviceState State { get; set; }
}