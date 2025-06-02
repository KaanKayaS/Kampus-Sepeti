using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.DTO
{
    public class AnnouncementDetailResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public string LocationName { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<CommentDetail> Comments { get; set; }
    }

    public class CommentDetail
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
