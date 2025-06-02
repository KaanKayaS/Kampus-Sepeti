using KampüsSepeti.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Commands.AnnouncementCommands
{
    public class CreateAnnouncementCommand : IRequest<Unit>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int LocationId { get; set; }
    }
}
