using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Modules.Identity.Endpoints.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class UsersController(ISender sender) : ControllerBase
    {

    }
}
