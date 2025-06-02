using FluentValidation;
using KampüsSepeti.Application.Features.Commands.RevokeCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Validator
{
    public class RevokeCommandValidator : AbstractValidator<RevokeCommand>
    {
        public RevokeCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();
        }
    }
}
