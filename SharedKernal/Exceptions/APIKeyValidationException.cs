namespace SharedKernal.Exceptions;

public sealed class APIKeyValidationException : Exception
{
    public APIKeyValidationException(string message = null) : base(message ?? "there is no data found")
    {
    }
}
