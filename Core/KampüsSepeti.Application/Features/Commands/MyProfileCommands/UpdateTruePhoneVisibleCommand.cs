using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Commands.MyProfileCommands
{
    public class UpdateTruePhoneVisibleCommand: IRequest<bool>
    {
        public int Id { get; set; }   // Userid

        public UpdateTruePhoneVisibleCommand(int id)
        {
            Id = id;
        }
    }
}
