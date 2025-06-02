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

    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreatePostCommandHandler(IUnitOfWork unitOfWork, LocationRules locationRules)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.GetWriteRepository<Post>().AddAsync(new Post
            {
                BrandName = request.BrandName,
                CategoryId = request.CategoryId,
                CreatedDate = DateTime.Now,
                Description = request.Description,
                Image = request.Image,
                LocationId = request.LocationId,
                Price = request.Price,
                UserId = request.UserId,
                Title = request.Title,
                StatusId = request.StatusId,
            });

            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
