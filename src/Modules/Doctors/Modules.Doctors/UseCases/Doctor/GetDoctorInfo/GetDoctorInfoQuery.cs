using FluentValidation;
using MediatR;
using SharedKernal.MediatR;
using SharedKernal.ResultResponse;

namespace Modules.Doctors.UseCases.Doctor.GetDoctorInfo
{
    internal sealed record GetDoctorInfoQueryRes(string nameEn, string nameAr, int age);
    internal sealed record GetDoctorInfoQuery : IQuery<GetDoctorInfoQueryRes>//handles only internal state
    {
        public int Id { get; set; }
    }
    internal sealed class GetDoctorInfoQueryValidator : AbstractValidator<GetDoctorInfoQuery>
    {
        public GetDoctorInfoQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
    internal sealed class GetDoctorInfoQueryHandler : IRequestHandler<GetDoctorInfoQuery, Result<GetDoctorInfoQueryRes>>
    {
        public async Task<Result<GetDoctorInfoQueryRes>> Handle(GetDoctorInfoQuery request, CancellationToken cancellationToken)
        {
            return new GetDoctorInfoQueryRes("ahmed", "wael", 22);
        }
    }
}
