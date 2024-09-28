using FluentValidation;


namespace Modules.Clients.UseCases.Client.Commands.AssignDoctorToClient
{
	internal sealed class AssignDoctorToClientValidator:AbstractValidator<AssignDoctorToClientCommand>
	{
        public AssignDoctorToClientValidator()
        {
            RuleFor(x=>x.ClientId).NotEmpty().NotNull().WithMessage("Client Id can't be null");
			RuleFor(x=>x.DoctorId).NotEmpty().NotNull();
			

			//search on other validations 
		}
	}
}
