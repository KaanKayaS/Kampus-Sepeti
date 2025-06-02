using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Commands.PostCommands
{
    public class RemovePostCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public RemovePostCommand(int id)
        {
            Id = id;
        }
    }
}
