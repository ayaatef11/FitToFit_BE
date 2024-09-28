using ApisContracts.Doctors;
using ApisContracts.Doctors.Dtos;
using FluentValidation;
using MediatR;
using SharedKernal.MediatR;
using SharedKernal.ResultResponse;

namespace Modules.Clients.UseCases.Client.Queries.GetClientInfo
{
    internal sealed record GetClientInfoRes(string name, int age, string doctorNameEn, string doctorNameAr, int doctorAge);
    internal sealed record GetClientInfoQuery : IQuery<GetClientInfoRes>// indicates that this query will return a result of type
                                                                        // GetClientInfoRes when it is executed.
    {
        public int Id { get; set; }
    }
    
   
}
