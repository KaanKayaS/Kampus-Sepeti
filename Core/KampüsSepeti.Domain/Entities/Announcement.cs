using KampüsSepeti.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KampüsSepeti.Domain.Entities
{
    public class Announcement : EntityBase 
    {
        public  string Title { get; set; }
        public  string Description { get; set; }
        public  int UserId { get; set; }
        public  int LocationId { get; set; }
        public Location Location { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
