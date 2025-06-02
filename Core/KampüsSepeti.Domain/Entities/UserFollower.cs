using KampüsSepeti.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Domain.Entities
{
    public class UserFollower 
    {
        public int FollowerId { get; set; } // Takip eden kullanıcı
        public User Follower { get; set; }

        public int FollowedId { get; set; } // Takip edilen kullanıcı
        public User Followed { get; set; }
        public DateTime FollowedDate { get; set; } // Takip etme zamanı
    }
}
