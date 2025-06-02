using FluentValidation;
using KampüsSepeti.Application.Features.Commands.AnnouncementCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Validator
{
    public class UpdateAnnouncementCommandValidator : AbstractValidator<UpdateAnnouncementCommand>
    {
        public UpdateAnnouncementCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithName("Başlık")
                .MaximumLength(25);


            RuleFor(x => x.Description)
                .NotEmpty()
                .WithName("Açıklama");


            RuleFor(x => x.LocationId)
                .GreaterThan(0)
                .WithName("Lokasyon ID");

        }
    }
}
