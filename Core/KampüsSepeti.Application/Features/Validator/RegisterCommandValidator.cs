using FluentValidation;
using KampüsSepeti.Application.Features.Commands.RegisterCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Validator
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(2)
                .WithName("İsim Soyisim");

            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(60)
                .EmailAddress()
                .MinimumLength(8)
                .WithName("E-Posta Adresi");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithName("Parola");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .MinimumLength(6)
                .Equal(x => x.Password)
                .WithMessage("'Parola Tekrarı',Parola değerine eşit olmalı.")
                .WithName("Parola Tekrarı");

            RuleFor(x => x.LocationId)
                .GreaterThan(0)
                .WithName("Lokasyon Id");


            RuleFor(x => x.UniversityId)
               .GreaterThan(0)
               .WithName("Üniversite Id");

            RuleFor(x => x.PhoneNumber)
               .NotEmpty()
               .Length(14)
               .WithName("Telefon numarası");



        }
    }
}
