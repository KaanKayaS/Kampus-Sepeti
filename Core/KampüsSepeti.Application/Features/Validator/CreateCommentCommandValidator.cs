using FluentValidation;
using KampüsSepeti.Application.Features.Commands.CommentCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Validator
{
    public class CreateCommentCommandValidator: AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.Content)
                .MinimumLength(5)
                .NotEmpty()
                .WithName("Yorum");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithName("Kullanıcı Id");

            RuleFor(x => x.AnnouncementId)
              .NotEmpty()
              .WithName("Duyuru Id");
        }
    }
}
