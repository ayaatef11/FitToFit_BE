using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Diagnostics;
//logging using serilog 
namespace SharedKernal.MediatR.Piplines
{
	// log the incoming commands and their results.
	public sealed class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>///takes the mediator request and response
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogDebug("[START] Handling HTTP request {RequestMethod} {RequestPath}",
            typeof(TRequest).Name, typeof(TResponse).Name);

            var timer = Stopwatch.StartNew();

            var response = await next();//see how long the request processsing takes

            timer.Stop();

            using (LogContext.PushProperty("TimeTaken", timer.Elapsed.Milliseconds))//where i can read these values
            using (LogContext.PushProperty("LogType", "PERFORMANCE"))
            {
                logger.LogInformation("[PERFORMANCE] The request {RequestMethod} {RequestPath} took {TimeTaken} seconds.",
                typeof(TRequest).Name, typeof(TResponse).Name, timer.Elapsed.Milliseconds);
            }
            logger.LogDebug("[END] Handled HTTP request {RequestMethod} {RequestPath}",// for detailed and diagnostic information t
                typeof(TRequest).Name, typeof(TResponse).Name);//why the request path is the type of response?
            return response;
        }
    }
}
