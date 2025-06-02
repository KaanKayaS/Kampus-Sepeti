using KampüsSepeti.Application.Features.Exceptions;
using KampüsSepeti.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Rules
{
    public class UserFollowersRules
    {
        public Task AlreadyFollowed(UserFollower userFollower)
        {
            if (userFollower != null)
                throw new AlreadyFollowedException();
            return Task.CompletedTask;
        }

        public Task UserFollowerNotFound(UserFollower userFollower)
        {
            if (userFollower == null)
                throw new UserFollowerNotFoundException();
            return Task.CompletedTask;
        }
    }
}
