using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernal.UnitOfWork;

namespace SharedKernal.MediatR.Piplines
{
    public sealed class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : notnull
    {
        private readonly ILogger<UnitOfWorkBehavior<TRequest, TResponse>> logger;
        private readonly IUOF _unitOfWork;

        public UnitOfWorkBehavior(ILogger<UnitOfWorkBehavior<TRequest, TResponse>> logger, IUOF unitOfWork)
        {
            this.logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IQueryResult)
                return await next();

            try
            {
                _unitOfWork.Begin();
                var response = await next();
                await _unitOfWork.Commit();
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "error happened during executing the command , going to rollback now ... ");

                await _unitOfWork.Rollback();

                throw;
            }
        }

    }
}
