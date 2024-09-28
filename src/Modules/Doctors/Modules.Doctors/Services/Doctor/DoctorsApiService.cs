using ApisContracts.Doctors;
using ApisContracts.Doctors.Dtos;
using MediatR;
using Modules.Doctors.UseCases.Doctor.Commands.CheckDoctorExists;
using Modules.Doctors.UseCases.Doctor.GetDoctorInfo;
using SharedKernal.ResultResponse;

namespace Modules.Doctors.Services.Doctor
{
    internal sealed class DoctorModuleService(ISender sender) : IDoctorModuleService
    {
        public async Task<Result<GetDoctorInfoByIdResDto>> GetDoctorInfoById(GetDoctorInfoByIdDtoRequest req)
        {
            return await sender.Send(new GetSummaryDoctorInfoQuery { Id = req.Id });
        }

        public async Task<Result<bool>> IsDoctorExists(int id)
        {
            return await sender.Send(new CheckDoctorExistsCommand { DoctorId = id });
        }

        
    }
}
