using FluentValidation;
using KampüsSepeti.Application.Features.Commands.LocationCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Validator
{
    public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithName("Lokasyon Adı");
                 
        }
    }
}
