using KampüsSepeti.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public  string FullName { get; set; }
        public string? Image { get; set; }
        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public int LocationId { get; set; }

        public  int UniversityId { get; set; }
        public bool IsPhoneVisible { get; set; }

        public Location Location { get; set; }

        public University University { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Announcement> Announcements { get; set; }
        public ICollection<FavoritePost> FavoritePosts { get; set; }

        public ICollection<UserFollower> Followers { get; set; } // Takipçiler
        public ICollection<UserFollower> Following { get; set; } // Takip ettikleri
    }
}
