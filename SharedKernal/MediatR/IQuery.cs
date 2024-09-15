using MediatR;
using SharedKernal.ResultResponse;

namespace SharedKernal.MediatR
{
    public interface IQueryResult { }
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>, IQueryResult
         where TResponse : notnull
    {
    }
    public interface IQuery : IRequest<Result>, IQueryResult
    {
    }

    public interface IRemoteQuery<TResponse> : IQuery<TResponse>
    {
    }
}
