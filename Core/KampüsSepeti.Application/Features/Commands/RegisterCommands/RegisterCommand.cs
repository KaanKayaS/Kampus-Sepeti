using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Commands.RegisterCommands
{
    public class RegisterCommand: IRequest<Unit>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int LocationId { get; set; }
        public int UniversityId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
