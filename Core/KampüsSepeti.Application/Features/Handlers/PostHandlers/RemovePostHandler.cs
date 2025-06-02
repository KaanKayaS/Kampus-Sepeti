using KampüsSepeti.Application.Features.Commands.PostCommands;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.PostHandlers
{
    public class RemovePostHandler : IRequestHandler<RemovePostCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly PostRules postRules;

        public RemovePostHandler(IUnitOfWork unitOfWork, PostRules postRules)
        {
            this.unitOfWork = unitOfWork;
            this.postRules = postRules;
        }
        public async Task<Unit> Handle(RemovePostCommand request, CancellationToken cancellationToken)
        {
            Post post = await unitOfWork.GetReadRepository<Post>().GetAsync(x => x.Id == request.Id);

            await postRules.PostNotFound(post);

            await unitOfWork.GetWriteRepository<Post>().HardDeleteAsync(post);

            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
