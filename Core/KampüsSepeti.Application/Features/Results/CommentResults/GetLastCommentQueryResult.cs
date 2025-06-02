using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Results.CommentResults
{
    public class GetLastCommentQueryResult
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public int UserId { get; set; }
        public string content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
