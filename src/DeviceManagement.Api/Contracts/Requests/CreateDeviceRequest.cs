using System.ComponentModel.DataAnnotations;
using DeviceManagement.Api.Domain.Enums;

namespace DeviceManagement.Api.Contracts.Requests;

public class CreateDeviceRequest
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = default!;

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Brand { get; set; } = default!;

    [Required]
    [EnumDataType(typeof(DeviceState))]
    public DeviceState State { get; set; }
}