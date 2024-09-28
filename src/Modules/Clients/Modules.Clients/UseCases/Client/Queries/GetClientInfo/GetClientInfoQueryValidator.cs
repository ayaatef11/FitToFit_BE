using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Clients.UseCases.Client.Queries.GetClientInfo
{
	internal sealed class GetClientInfoQueryValidator : AbstractValidator<GetClientInfoQuery>
	{
		public GetClientInfoQueryValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
		}
	}
}
