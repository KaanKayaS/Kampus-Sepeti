using KampüsSepeti.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Domain.Entities
{
    public class Post : EntityBase
    {
        public string Title { get; set; }
        public string? Image { get; set; }
        public string Description { get; set; }
        public string? BrandName { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
        public int LocationId { get; set; }
        public int StatusId { get; set; }
        public int CategoryId { get; set; }
        public User User { get; set; }
        public Location Location { get; set; }
        public Status Status { get; set; }
        public Category Category { get; set; }
        public ICollection<FavoritePost> FavoritedByUser { get; set; }

    }
}
