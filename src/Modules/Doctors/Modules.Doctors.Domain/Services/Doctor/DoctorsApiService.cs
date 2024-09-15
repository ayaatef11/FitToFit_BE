using MediatR;
using Modules.Doctors.ApiContracts.Doctor;
using Modules.Doctors.ApiContracts.Doctor.Dtos;
using Modules.Doctors.UseCases.Doctor.GetDoctorInfo;
using SharedKernal.ResultResponse;

namespace Modules.Doctors.Services.Doctor
{
    internal sealed class DoctorsApiService(ISender sender) : IDoctorService
    {
        public async Task<Result<GetDoctorInfoByIdResDto>> GetDoctorInfoById(GetDoctorInfoByIdDtoRequest req)
        {
            return await sender.Send(new GetSummaryDoctorInfoQuery { Id = req.Id });
        }
    }
}
