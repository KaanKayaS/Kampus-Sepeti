using KampüsSepeti.Application.Features.Results.LoginResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Commands.LoginCommands
{
    public class LoginCommand: IRequest<LoginCommandResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
