﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Commands.AnnouncementCommands
{
    public class RemoveAnnouncementCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public RemoveAnnouncementCommand(int id)
        {
            Id = id;
        }
    }
}
