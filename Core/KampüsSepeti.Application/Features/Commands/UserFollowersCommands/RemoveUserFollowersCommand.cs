using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Commands.UserFollowersCommands
{
    public class RemoveUserFollowersCommand : IRequest<Unit>
    {
        public int FollowerId { get; set; }
        public int FollowedId { get; set; }

        public RemoveUserFollowersCommand(int followedId, int followerId)
        {
            FollowerId = followerId;
            FollowedId = followedId;
        }
    }
}
