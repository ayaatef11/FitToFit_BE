using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Clients.UseCases.Client.GetClientInfo;

namespace Modules.Clients.Endpoints.Clients
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ClientsController(ISender sender) : ControllerBase
    {
        [HttpGet("{id:int}/info")]
        [ProducesResponseType(typeof(GetClientInfoRes), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetClientInfo([FromRoute] int id)
        {
            var res = await sender.Send(new GetClientInfoQuery { Id = id });

            return Ok(res);
        }
    }
}
