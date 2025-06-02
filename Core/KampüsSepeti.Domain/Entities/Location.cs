using KampüsSepeti.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Domain.Entities
{
    public class Location: EntityBase
    {
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Announcement> Announcements { get; set; }
    }
}
