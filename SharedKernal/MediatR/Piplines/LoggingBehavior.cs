using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Diagnostics;

namespace SharedKernal.MediatR.Piplines
{
    public sealed class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogDebug("[START] Handling HTTP request {RequestMethod} {RequestPath}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = Stopwatch.StartNew();

            var response = await next();

            timer.Stop();

            using (LogContext.PushProperty("TimeTaken", timer.Elapsed.Milliseconds))
            using (LogContext.PushProperty("LogType", "PERFORMANCE"))
            {
                logger.LogInformation("[PERFORMANCE] The request {RequestMethod} {RequestPath} took {TimeTaken} seconds.",
                typeof(TRequest).Name, timer.Elapsed.Milliseconds);
            }

            logger.LogDebug("[END] Handled HTTP request {RequestMethod} {RequestPath}",
                typeof(TRequest).Name, typeof(TResponse).Name);
            return response;
        }
    }
}
