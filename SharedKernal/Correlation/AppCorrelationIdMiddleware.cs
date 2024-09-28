using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace SharedKernal.Correlation
{//tracking
    public sealed class AppCorrelationIdMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            if (!context.Request.Headers.TryGetValue(SharedConstants.CrossCutting.CorrelationIdHeader, out var correlationId))
            {
                if (string.IsNullOrEmpty(correlationId))
                    correlationId = Guid.NewGuid().ToString();

                context.Request.Headers[SharedConstants.CrossCutting.CorrelationIdHeader] = correlationId;
            }

            context.Response.OnStarting(() =>
            {
                context.Response.Headers[SharedConstants.CrossCutting.CorrelationIdHeader] = correlationId;
                return Task.CompletedTask;
            });

            using (LogContext.PushProperty(SharedConstants.CrossCutting.LogCorrelationId, correlationId[0]))
            {
                await next(context);
            }
        }
    }
}
