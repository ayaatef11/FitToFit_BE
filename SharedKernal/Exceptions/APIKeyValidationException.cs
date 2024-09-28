namespace SharedKernal.Exceptions;

public sealed class APIKeyValidationException(string? message ) : Exception(message ?? "there is no data found")
{

}
