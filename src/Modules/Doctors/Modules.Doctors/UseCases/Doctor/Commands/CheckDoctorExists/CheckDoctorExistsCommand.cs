using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Modules.Doctors.Data;
using Modules.Doctors.UseCases.Doctor.GetDoctorInfo;
using SharedKernal.MediatR;
using SharedKernal.ResultResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Doctors.UseCases.Doctor.Commands.CheckDoctorExists
{
    //internal sealed record CheckDoctorExistsRes(bool exists);
    internal sealed record CheckDoctorExistsCommand:IQuery</*CheckDoctorExistsRes*/bool>
    {
        public int DoctorId { get; set; }
    }

    internal sealed class CheckDoctorExistsHandler(DoctorsDbContext db) : IRequestHandler<CheckDoctorExistsCommand, Result</*CheckDoctorExistsRes*/bool>>
    {


        public async  Task<Result</*CheckDoctorExistsRes*/bool>> Handle(CheckDoctorExistsCommand request, CancellationToken cancellationToken)
        {
            var result = await db.Doctors.FirstOrDefaultAsync(x => x.Id == request.DoctorId, cancellationToken);
            if (result != null) return true;//return Result.Success();
            return false;// Result.Failure("Specified doctor isn't found ");
                /*return new CheckDoctorExistsRes(true);
            return new CheckDoctorExistsRes(false);*/
        }
    }


}
