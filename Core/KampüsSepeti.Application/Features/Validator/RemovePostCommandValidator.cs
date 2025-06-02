using FluentValidation;
using KampüsSepeti.Application.Features.Commands.PostCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Validator
{
    public class RemovePostCommandValidator : AbstractValidator<RemovePostCommand>
    {
        public RemovePostCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithName("Id");
        }
    }
}
