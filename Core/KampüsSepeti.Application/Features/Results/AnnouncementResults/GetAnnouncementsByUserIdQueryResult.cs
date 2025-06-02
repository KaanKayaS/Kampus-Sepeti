using KampüsSepeti.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Results.AnnouncementResults
{
    public class GetAnnouncementsByUserIdQueryResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LocationName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CommentCount { get; set; }
    }
}
