using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Commands.MyProfileCommands
{
    public class UpdateNullProfilePhotoCommand : IRequest<Unit>
    {
        public int Id { get; set; }   // Userid

        public UpdateNullProfilePhotoCommand(int id)
        {
            Id = id;
        }
    }
}
