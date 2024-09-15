using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Doctors.UseCases.Doctor.GetDoctorInfo;

namespace Modules.Doctors.Endpoints.Doctors
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class DoctorsController(ISender sender) : ControllerBase
    {
        [HttpGet("{id:int}/info")]
        [ProducesResponseType(typeof(GetDoctorInfoQueryRes), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDoctorInfo([FromRoute] int id)
        {
            var res = await sender.Send(new GetDoctorInfoQuery { Id = id });

            return Ok(res);
        }
    }
}
