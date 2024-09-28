using ApisContracts.Doctors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Clients.Data;
using SharedKernal.ResultResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Clients.UseCases.Client.Commands.AssignDoctorToClient
{
    //we are here in the client
    internal class AssignDoctorToClientHandler(ClientsDbContext _context, IDoctorModuleService _doctorModuleService) : IRequestHandler<AssignDoctorToClientCommand, Result<bool>>
    {
        
        public async Task<Result<bool>> Handle(AssignDoctorToClientCommand request, CancellationToken cancellationToken)
        {
            //are those validations handled in the 
           var client=await _context.Clients.FirstOrDefaultAsync(x=>x.Id==request.ClientId);
            var doctor = _doctorModuleService.IsDoctorExists(request.DoctorId);
            //the assignment process
            if (doctor != null) { client.DoctorId = request.DoctorId;
                return true;//Result.Success();
            }
            return false;//Result.Failure("Doctor isn't found");
        }
    }
}
