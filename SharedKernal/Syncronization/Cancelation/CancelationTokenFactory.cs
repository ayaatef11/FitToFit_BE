using Microsoft.AspNetCore.Http;

namespace SharedKernal.Syncronization.Cancelation
{
    public sealed class CancelationTokenFactory(IHttpContextAccessor httpContextAccessor) : ICancelationTokenFactory
    {
        public CancellationToken CancellationToken { get; }

        public CancellationToken GetCancellationToken()
        {
            return httpContextAccessor.HttpContext?.RequestAborted ?? CancellationToken.None;
        }
    }
}
