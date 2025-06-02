using FluentValidation;
using KampüsSepeti.Application.Features.Commands.CommentCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Validator
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithName("Yorum");

            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
