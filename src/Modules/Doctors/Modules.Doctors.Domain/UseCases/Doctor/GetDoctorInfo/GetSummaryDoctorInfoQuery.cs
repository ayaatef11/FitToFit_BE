using MediatR;
using Modules.Doctors.ApiContracts.Doctor.Dtos;
using SharedKernal.MediatR;
using SharedKernal.ResultResponse;

namespace Modules.Doctors.UseCases.Doctor.GetDoctorInfo
{
    internal sealed class GetSummaryDoctorInfoQuery : IQuery<GetDoctorInfoByIdResDto>
    {
        public int Id { get; set; }
    }
    internal sealed class GetSummaryDoctorInfoQueryHandler : IRequestHandler<GetSummaryDoctorInfoQuery, Result<GetDoctorInfoByIdResDto>>
    {
        public async Task<Result<GetDoctorInfoByIdResDto>> Handle(GetSummaryDoctorInfoQuery request, CancellationToken cancellationToken)
        {
            return new GetDoctorInfoByIdResDto
            {
                Id = request.Id,
                Age = 22,
                NameAr = "rrr",
                NameEn = "tttt"
            };
        }
    }
}
