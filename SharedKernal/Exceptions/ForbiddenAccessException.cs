namespace SharedKernal.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException(string message) : base(message ?? "entity has no access to this resource")
    {
    }
}
