using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Hubs
{
    public class FollowHub : Hub
    {
        public async Task UpdateFollowStatus(int followerId, int followedId, bool isFollowed)
        {
            await Clients.All.SendAsync("ReceiveFollowUpdate", followerId, followedId, isFollowed);
        }
    }
}
