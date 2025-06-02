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
    public class RemoveCommentCommandHandler : IRequestHandler<RemoveCommentCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly CommentRules commentRules;

        public RemoveCommentCommandHandler(IUnitOfWork unitOfWork, CommentRules commentRules)
        {
            this.unitOfWork = unitOfWork;
            this.commentRules = commentRules;
        }
        public async Task<Unit> Handle(RemoveCommentCommand request, CancellationToken cancellationToken)
        {
            Comment comment = await unitOfWork.GetReadRepository<Comment>().GetAsync(x => x.Id == request.Id);
            await commentRules.CommentNoutFound(comment);

            await unitOfWork.GetWriteRepository<Comment>().HardDeleteAsync(comment);

            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
