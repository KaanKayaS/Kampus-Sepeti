using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Commands.MyProfileCommands
{
    public class UpdateFalsePhoneVisibleCommand : IRequest<bool>
    {
        public int Id { get; set; }   // Userid

        public UpdateFalsePhoneVisibleCommand(int id)
        {
            Id = id;
        }
    }
}
