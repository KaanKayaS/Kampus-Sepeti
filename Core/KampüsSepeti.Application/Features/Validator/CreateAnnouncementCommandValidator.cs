using FluentValidation;
using KampüsSepeti.Application.Features.Commands.AnnouncementCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Validator
{
    public class CreateAnnouncementCommandValidator : AbstractValidator<CreateAnnouncementCommand>
    {
        public CreateAnnouncementCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Duyuru Başlığı boş bırakılmamalıdır");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Duyuru Açıklaması boş bırakılmamalıdır");


        }
    }
}
