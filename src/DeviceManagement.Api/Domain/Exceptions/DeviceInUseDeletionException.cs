namespace DeviceManagement.Api.Domain.Exceptions;

public class DeviceInUseDeletionException : BusinessRuleException
{
    public DeviceInUseDeletionException()
        : base("A device in use cannot be deleted.")
    {
    }
}