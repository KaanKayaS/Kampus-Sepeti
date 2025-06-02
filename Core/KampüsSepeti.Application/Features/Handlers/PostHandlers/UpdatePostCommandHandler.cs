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
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly PostRules postRules;

        public UpdatePostCommandHandler(IUnitOfWork unitOfWork, PostRules postRules)
        {
            this.unitOfWork = unitOfWork;
            this.postRules = postRules;
        }

        public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            Post post = await unitOfWork.GetReadRepository<Post>().GetAsync(x => x.Id == request.Id);

            await postRules.PostNotFound(post);

            if (post.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException("Bu ilanı düzenleme yetkiniz bulunmamaktadır.");
            }

            post.Title = request.Title;
            post.Description = request.Description;
            post.LocationId = request.LocationId;
            post.Price = request.Price;
            post.StatusId = request.StatusId;
            post.BrandName = request.BrandName;
            post.CategoryId = request.CategoryId;
            post.Image = request.Image;

            await unitOfWork.GetWriteRepository<Post>().UpdateAsync(post);
            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
