

namespace Modules.Clients.Endpoints.Clients.Dtos
{
	
	public record  AssignDoctorToClientRequestDto//contract data only has primitive types 
	{
		public int DoctorId { get; init; }//allows the property to be set only during object
										  //initialization and then it is readonly  
	}
}
