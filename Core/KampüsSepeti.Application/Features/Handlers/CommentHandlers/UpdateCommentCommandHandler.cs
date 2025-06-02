using KampüsSepeti.Application.Features.Commands.CommentCommands;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.CommentHandlers
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly CommentRules commentRules;
        public UpdateCommentCommandHandler(IUnitOfWork unitOfWork, CommentRules commentRules)
        {
            this.unitOfWork = unitOfWork;
            this.commentRules = commentRules;
        }
        public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            Comment comment = await unitOfWork.GetReadRepository<Comment>().GetAsync(x => x.Id == request.Id);
            await commentRules.CommentNoutFound(comment);

            comment.Content = request.Content;

            await unitOfWork.GetWriteRepository<Comment>().UpdateAsync(comment);

            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
