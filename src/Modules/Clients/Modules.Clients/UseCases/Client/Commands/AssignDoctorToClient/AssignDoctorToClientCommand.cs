using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using System.Windows.Input;
using SharedKernal.MediatR;//to use the custom one 
namespace Modules.Clients.UseCases.Client.Commands.AssignDoctorToClient
{
    internal sealed class AssignDoctorToClientCommand : ICommand<bool>//it is immutable so why not to be record 
    {
        public int DoctorId { get; set; }
        public int ClientId { get; set; }

    }


}
