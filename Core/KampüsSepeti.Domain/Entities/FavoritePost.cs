using KampüsSepeti.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Domain.Entities
{
    public class FavoritePost: EntityBase
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
    }
}
