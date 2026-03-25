using System.ComponentModel.DataAnnotations;
using DeviceManagement.Api.Domain.Enums;

namespace DeviceManagement.Api.Contracts.Requests;

public class PatchDeviceRequest
{
    [MaxLength(100)]
    public string? Name { get; set; }

    [MaxLength(100)]
    public string? Brand { get; set; }

    [EnumDataType(typeof(DeviceState))]
    public DeviceState? State { get; set; }
}