using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Results.CommentResults
{
    public class GetCommentsByAnnouncementIdQueryResult
    {
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
