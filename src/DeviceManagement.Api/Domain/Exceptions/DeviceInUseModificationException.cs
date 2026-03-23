namespace DeviceManagement.Api.Domain.Exceptions;

public class DeviceInUseModificationException : BusinessRuleException
{
    public DeviceInUseModificationException()
        : base("Name and brand cannot be changed when the device is in use.")
    {
    }
}