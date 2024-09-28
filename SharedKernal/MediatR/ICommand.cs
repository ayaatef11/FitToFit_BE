using MediatR;
using SharedKernal.ResultResponse;

namespace SharedKernal.MediatR
{
    //why???
    public interface ICommandResult { }
    public interface ICommand<TResponse> : IRequest<Result<TResponse>>, ICommandResult
    {
    }
    public interface ICommand : IRequest<Result>, ICommandResult
    {
    }
}
