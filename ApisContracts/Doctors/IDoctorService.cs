using Modules.Doctors.ApiContracts.Doctor.Dtos;
using SharedKernal.ResultResponse;

namespace Modules.Doctors.ApiContracts.Doctor
{
    public interface IDoctorService
    {
        Task<Result<GetDoctorInfoByIdResDto>> GetDoctorInfoById(GetDoctorInfoByIdDtoRequest req);
    }
}
