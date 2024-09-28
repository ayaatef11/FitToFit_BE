using SharedKernal.ResultResponse;

namespace SharedKernal.Exceptions;
//why not separate them between classes?
public abstract class NotFoundException : Exception
{
    public string? ErrorMessage { get; }
    public Exception? Exception { get; }

    protected NotFoundException(string? errorMessage = null, Exception? innerException = null) : base(errorMessage ?? "Entity not found", innerException)
    {
        ErrorMessage = errorMessage;
        Exception = innerException;
    }
}
public sealed class BusinessException : Exception
{
    public string ErrorMessage { get; }
    public Exception? Exception { get; }
    public Result Result { get; }

    public BusinessException(string errorMessage, Result result, Exception? innerException = null) : base(errorMessage ?? "Business exception", innerException)
    {
        Result = result;
        ErrorMessage = errorMessage;
        Exception = innerException;
    }
}
