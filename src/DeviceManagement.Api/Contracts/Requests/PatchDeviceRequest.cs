using System.ComponentModel.DataAnnotations;
using DeviceManagement.Api.Domain.Enums;

namespace DeviceManagement.Api.Contracts.Requests;

public class PatchDeviceRequest
{
    [StringLength(100, MinimumLength = 1)]
    public string? Name { get; set; }

    [StringLength(100, MinimumLength = 1)]
    public string? Brand { get; set; }

    [EnumDataType(typeof(DeviceState))]
    public DeviceState? State { get; set; }
}