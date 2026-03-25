using System.ComponentModel.DataAnnotations;
using DeviceManagement.Api.Domain.Enums;

namespace DeviceManagement.Api.Contracts.Requests;

public class CreateDeviceRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Brand { get; set; } = string.Empty;

    [Required]
    [EnumDataType(typeof(DeviceState))]
    public DeviceState State { get; set; }
}