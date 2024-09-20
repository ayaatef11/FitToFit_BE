using ApisContracts.Doctors;
using ApisContracts.Doctors.Dtos;
using FluentValidation;
using MediatR;
using SharedKernal.MediatR;
using SharedKernal.ResultResponse;

namespace Modules.Clients.UseCases.Client.GetClientInfo
{
    internal sealed record GetClientInfoRes(string name, int age, string doctorNameEn, string doctorNameAr, int doctorAge);
    internal sealed record GetClientInfoQuery : IQuery<GetClientInfoRes>
    {
        public int Id { get; set; }
    }
    internal sealed class GetClientInfoQueryValidator : AbstractValidator<GetClientInfoQuery>
    {
        public GetClientInfoQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
    internal sealed class GetClientInfoQueryHandler(IDoctorModuleService doctorService) : IRequestHandler<GetClientInfoQuery, Result<GetClientInfoRes>>
    {
        public async Task<Result<GetClientInfoRes>> Handle(GetClientInfoQuery request, CancellationToken cancellationToken)
        {
            var doctorOfClientRes = await doctorService.GetDoctorInfoById(new GetDoctorInfoByIdDtoRequest
            {
                Id = 44
            });

            doctorOfClientRes.EnsureSuccess("failed to get doctor info");

            return new GetClientInfoRes("patient", 11, doctorOfClientRes.Data.NameEn, doctorOfClientRes.Data.NameAr, doctorOfClientRes.Data.Age);
        }
    }
}
