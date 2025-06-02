using KampüsSepeti.Application.Features.Commands.UserFollowersCommands;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Application.Hubs;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.UserFollowersHandlers
{
    public class RemoveUserFollowersCommandHandler : IRequestHandler<RemoveUserFollowersCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserRules userRules;
        private readonly UserManager<User> userManager;
        private readonly UserFollowersRules userFollowersRules;
        private readonly IHubContext<FollowHub> hubContext;

        public RemoveUserFollowersCommandHandler(IUnitOfWork unitOfWork,
            UserRules userRules, UserManager<User> userManager, UserFollowersRules userFollowersRules
            ,IHubContext<FollowHub> hubContext)
        {
            this.unitOfWork = unitOfWork;
            this.userRules = userRules;
            this.userManager = userManager;
            this.userFollowersRules = userFollowersRules;
            this.hubContext = hubContext;
        }
        public async Task<Unit> Handle(RemoveUserFollowersCommand request, CancellationToken cancellationToken)
        {
            User? Followeruser = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.FollowerId);
            await userRules.UserMustNotBeNull(Followeruser);

            User? Followeduser = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.FollowedId);
            await userRules.UserMustNotBeNull(Followeduser);

            UserFollower userFollowers = await unitOfWork.GetReadRepository<UserFollower>()
               .GetAsync(x => x.FollowerId == request.FollowerId && x.FollowedId == request.FollowedId);
            await userFollowersRules.UserFollowerNotFound(userFollowers);

            await unitOfWork.GetWriteRepository<UserFollower>().HardDeleteAsync(userFollowers);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
