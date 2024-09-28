namespace SharedKernal.Exceptions;

public class ForbiddenAccessException(string message) : Exception(message ?? "entity has no access to this resource")
{
}
