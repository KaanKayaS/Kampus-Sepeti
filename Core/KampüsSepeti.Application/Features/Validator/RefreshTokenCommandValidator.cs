using FluentValidation;
using KampüsSepeti.Application.Features.Commands.RefreshTokenCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Validator
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.AccessToken).NotEmpty();

            RuleFor(x => x.RefreshToken).NotEmpty();
            
        }
    }
}
