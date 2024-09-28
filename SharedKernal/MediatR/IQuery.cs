using MediatR;
using SharedKernal.ResultResponse;

namespace SharedKernal.MediatR
{
    //not undersstand it 
    public interface IQueryResult { }// identify types that represent a query result. 
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>, IQueryResult
         where TResponse : notnull//It's a way to enforce non-nullability for type safety.
    {
    }
    public interface IQuery : IRequest<Result>, IQueryResult
    {
    }

    public interface IRemoteQuery<TResponse> : IQuery<TResponse>
    {
    }
}
