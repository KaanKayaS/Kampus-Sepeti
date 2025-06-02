using KampüsSepeti.Application.Bases;
using KampüsSepeti.Application.Features.Exceptions;
using KampüsSepeti.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Rules
{
    public class FavoritePostRules : BaseRules
    {
        public Task AlreadyAdded(int PostId, int UserId, IList<FavoritePost> favorite)
        {

           foreach(var item in favorite)
            {
                if (item.UserId == UserId && item.PostId == PostId)
                {
                       throw new AlreadyAddedException();
                   
                }

            }
            return Task.CompletedTask;
        }

        public async Task YouCanNotAddYourOwnPost(User currentUser, int postId)
        {
            IList<Post> posts = currentUser.Posts.ToList();

            foreach (var post in posts) 
            {
                if(post.Id == postId) 
                {
                    throw new YouCanNotAddYourOwnPostException();
                }
            }
        }

        public async Task NotFoundFavoritePost(int userId , int postId, IList<FavoritePost> favorites)
        {
            bool exists = favorites.Any(f => f.UserId == userId && f.PostId == postId);

            if (!exists)
            {
                throw new NotFoundFavoritePostException();
            }
        }
    }
}
