using ApisContracts.Doctors.Dtos;
using SharedKernal.ResultResponse;

namespace ApisContracts.Doctors
{
    public interface IDoctorModuleService
    {
        Task<Result<GetDoctorInfoByIdResDto>> GetDoctorInfoById(GetDoctorInfoByIdDtoRequest req);
    }
}
