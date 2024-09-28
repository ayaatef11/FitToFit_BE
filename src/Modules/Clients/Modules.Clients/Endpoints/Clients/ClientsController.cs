using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Clients.Endpoints.Clients.Dtos;
using Modules.Clients.UseCases.Client.Commands.AssignDoctorToClient;
using Modules.Clients.UseCases.Client.Queries.GetClientInfo;
using SharedKernal.UserPrincipals;
using System.Security.Claims;

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
            //in process communication between components decoubly
            //presentation layer calls the service layer without knowing the service layer or the service layer knows me 
            var res = await sender.Send(new GetClientInfoQuery { Id = id });///we have the ISender and the IPublisher

            return Ok(res);
        }
        //this is a command operation
        [HttpPost("Assign_doctor_to_client")]
        public async Task<IActionResult> AssignDoctorToClient([FromBody]AssignDoctorToClientRequestDto assignDoctorToClientRequestDto)
        {
            //here we send the usecase
            var res=await sender.Send(new AssignDoctorToClientCommand { 
                DoctorId=assignDoctorToClientRequestDto.DoctorId ,
                ClientId= 1//User.GetId()//we here has declarative , we have abstrated the logic
            
            });
            return Ok(res);
        }
    }
}
