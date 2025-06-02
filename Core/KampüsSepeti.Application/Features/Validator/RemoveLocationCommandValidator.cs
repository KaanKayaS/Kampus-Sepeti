using FluentValidation;
using KampüsSepeti.Application.Features.Commands.LocationCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Validator
{
    public class RemoveLocationCommandValidator : AbstractValidator<RemoveLocationCommand>
    {
        public RemoveLocationCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)     
                .WithName("Id");
        }
    }
}
