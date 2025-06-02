using KampüsSepeti.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Commands.MyProfileCommands
{
    public class UpdateProfilePhotoCommand: IRequest<Unit>
    {
        public int UserId { get; set; }
        public IFormFile ProfilePhoto { get; set; }
    }
}
