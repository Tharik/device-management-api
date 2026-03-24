using DeviceManagement.Api.Domain.Enums;

namespace DeviceManagement.Api.Contracts.Requests;

public class PatchDeviceRequest
{
    public string? Name { get; set; }

    public string? Brand { get; set; }

    public DeviceState? State { get; set; }
}