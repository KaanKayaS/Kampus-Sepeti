using KampüsSepeti.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Domain.Entities
{
    public class Comment : EntityBase
    {
        public  string Content { get; set; }
        public  int UserId { get; set; }
        public  int AnnouncementId { get; set; }
        public User User { get; set; }
        public Announcement Announcement { get; set; }
    }
}
