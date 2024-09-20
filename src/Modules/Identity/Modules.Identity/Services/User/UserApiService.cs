using MediatR;
using Modules.Identity.ApiContracts;

namespace Modules.Identity.Services.User
{
    internal sealed class UserApiService(ISender sender) : IUserService
    {
    }
}
