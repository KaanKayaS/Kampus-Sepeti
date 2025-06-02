using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.DTO
{
    public class AnnouncementDto
    {
        public string Title { get; set; }
        public string Username { get; set; }
        public string Location { get; set; }
        public int CommentCount { get; set; }
    }
}
