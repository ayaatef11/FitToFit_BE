using Microsoft.AspNetCore.Mvc;

namespace SharedKernal.ResultResponse
{
    public sealed class Result : Result<object>
    {
        public Result() // only for serializing
        {
        }

        internal Result(bool succeeded, string? message = null) : base(succeeded, message)
        {
        }

        public static new Result Success(string? message = null)
        {
            return new Result(true)
            {
                Message = message
            };
        }

        public static new Result Failure(string message)
        {
            return new Result(false, message);
        }

        public static new Result Failure(ProblemDetails problemDetails)
        {
            return new Result(false)
            {
                ProblemDetails = problemDetails
            };
        }
    }

    public class Result<TData>
    {
        public Result() // only for serializing
        {
        }

        internal Result(bool succeeded, string? message = null)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public bool Succeeded { get; init; }

        public string Message { get; set; }
        public TData Data { get; set; }
        public ProblemDetails ProblemDetails { get; set; }

        public static Result<TData> Success(string? message = null)
        {
            return new Result<TData>(true)
            {
                Message = message
            };
        }

        public static Result<TData> Failure(string message)
        {
            return new Result<TData>(false, message);
        }

        public static Result<TData> Failure(ProblemDetails problemDetails)
        {
            return new Result<TData>(false)
            {
                ProblemDetails = problemDetails
            };
        }

        public Result<TData> WithData(TData data)
        {
            Data = data;
            return this;
        }

        public static implicit operator Result(Result<TData> result)
            => new Result
            {
                Succeeded = result.Succeeded,
                Message = result.Message,
                ProblemDetails = result.ProblemDetails
            };
        public static implicit operator Result<TData>(Result<object> result)
            => new Result<TData>
            {
                Succeeded = result.Succeeded,
                Message = result.Message,
                ProblemDetails = result.ProblemDetails
            };

        public static implicit operator Result<TData>(TData data)
           => new Result<TData>
           {
               Succeeded = true,
               Data = data
           };
    }
}
