using ApisContracts.Doctors.Dtos;
using ApisContracts.Doctors;
using MediatR;
using SharedKernal.ResultResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Clients.UseCases.Client.Queries.GetClientInfo
{
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
