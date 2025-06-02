using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Commands.LocationCommands
{
    public class RemoveLocationCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public RemoveLocationCommand(int id)
        {
            Id = id;
        }
    }
}
