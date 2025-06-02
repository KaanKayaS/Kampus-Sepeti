using KampüsSepeti.Application.DTOs;
using KampüsSepeti.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Results.AnnouncementResults
{
    public class GetAnnouncementByIdQueryResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string UserImage { get; set; }
        public string LocationName { get; set; }
        public int LocationID { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<CommentDto> Comments { get; set; }    
    }
}
