using KampüsSepeti.Application.Features.Commands.FavoritePostCommands;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.FavoritePostHandlers
{
    public class AddFavoritePostCommandHandler : IRequestHandler<AddFavoritePostCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserRules userRules;
        private readonly PostRules postRules;
        private readonly UserManager<User> userManager;
        private readonly FavoritePostRules favoritePostRules;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AddFavoritePostCommandHandler(IUnitOfWork unitOfWork, UserRules userRules,
            PostRules postRules, UserManager<User> userManager, FavoritePostRules favoritePostRules,
            IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.userRules = userRules;
            this.postRules = postRules;
            this.userManager = userManager;
            this.favoritePostRules = favoritePostRules;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(AddFavoritePostCommand request, CancellationToken cancellationToken)
        {
            User? user = userManager.Users.FirstOrDefault(x => x.Id == request.UserId);
            await userRules.UserMustNotBeNull(user);

            Post post = await unitOfWork.GetReadRepository<Post>().GetAsync(x => x.Id == request.PostId);
            await postRules.PostNotFound(post);


            User? currentUsers = await userManager.Users
                .Include(u => u.Posts)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);

            await favoritePostRules.YouCanNotAddYourOwnPost(currentUsers, request.PostId);

            IList<FavoritePost> favorites = await unitOfWork.GetReadRepository<FavoritePost>().GetAllAsync();
            await favoritePostRules.AlreadyAdded(request.PostId,request.UserId,favorites);

            await unitOfWork.GetWriteRepository<FavoritePost>().AddAsync(new FavoritePost
            {
                PostId = request.PostId,
                UserId = request.UserId,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            });

            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
