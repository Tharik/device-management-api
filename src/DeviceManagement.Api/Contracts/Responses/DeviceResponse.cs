namespace DeviceManagement.Api.Contracts.Responses;

public class DeviceResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}