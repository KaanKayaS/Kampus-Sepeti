using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Commands.CommentCommands
{
    public class CreateCommentCommand : IRequest<Unit>
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public int AnnouncementId { get; set; }
    }
}
