using KampüsSepeti.Application.Features.Commands.FavoritePostCommands;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.FavoritePostHandlers
{
    public class RemoveFavoritePostCommandHandler : IRequestHandler<RemoveFavoritePostCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<User> userManager;
        private readonly UserRules userRules;
        private readonly PostRules postRules;
        private readonly FavoritePostRules favoritePostRules;
        public RemoveFavoritePostCommandHandler(IUnitOfWork unitOfWork,
            UserManager<User> userManager, UserRules userRules, PostRules postRules
            , FavoritePostRules favoritePostRules)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.userRules = userRules;
            this.postRules = postRules;
            this.favoritePostRules = favoritePostRules;
        }
        public async Task<Unit> Handle(RemoveFavoritePostCommand request, CancellationToken cancellationToken)
        {
            User? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

            await userRules.UserMustNotBeNull(user);

            Post post = await unitOfWork.GetReadRepository<Post>().GetAsync(x => x.Id == request.PostId);
            await postRules.PostNotFound(post);

            IList<FavoritePost> favorites = await unitOfWork.GetReadRepository<FavoritePost>().GetAllAsync();

            FavoritePost deletedfavoritePost = await unitOfWork.GetReadRepository<FavoritePost>()
                .GetAsync(x => x.UserId ==  request.UserId && x.PostId == request.PostId);

            await favoritePostRules.NotFoundFavoritePost(user.Id, post.Id, favorites);

            await unitOfWork.GetWriteRepository<FavoritePost>().HardDeleteAsync(deletedfavoritePost);

            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
