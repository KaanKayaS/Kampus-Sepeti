using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Commands.CommentCommands
{
    public class RemoveCommentCommand: IRequest<Unit>
    {
        public int Id { get; set; }

        public RemoveCommentCommand(int id)
        {
            Id = id;
        }
    }
}
