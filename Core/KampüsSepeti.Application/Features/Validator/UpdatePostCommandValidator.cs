using FluentValidation;
using KampüsSepeti.Application.Features.Commands.PostCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Validator
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithName("Başlık");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithName("Açıklama");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Fiyat değeri 0'dan büyük olmalıdır");

            RuleFor(x => x.StatusId)
                .GreaterThan(0)
                .WithName("Durum ID");

            RuleFor(x => x.LocationId)
                .GreaterThan(0)
                .WithName("Lokasyon ID");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithName("Kategori ID");

        }
    }
}
