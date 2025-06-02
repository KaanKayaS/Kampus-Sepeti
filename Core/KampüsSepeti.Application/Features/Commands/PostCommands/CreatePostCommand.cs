using KampüsSepeti.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Commands.PostCommands
{
    public class CreatePostCommand : IRequest<Unit>
    {
        public string Title { get; set; }
        public string? Image { get; set; }
        public string Description { get; set; }
        public string? BrandName { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
        public int LocationId { get; set; }
        public int StatusId { get; set; }
        public int CategoryId { get; set; }
    }
}
