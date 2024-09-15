using SharedKernal.Exceptions;

namespace SharedKernal.ResultResponse
{
    public static class ResultUtility
    {
        public static Result<T> ToResult<T>(this Exception e)
        {
            var message = e.Message;
            if (e.InnerException is not null)
                message = $"{message} - inner details => {e.InnerException.Message}";

            return Result<T>.Failure(message);
        }

        public static void EnsureSuccess<T>(this Result<T> r, string errorMessage = null)
        {
            if (!r.Succeeded)
                throw new BusinessException(errorMessage ?? "received not success response", r);
        }
    }
}
