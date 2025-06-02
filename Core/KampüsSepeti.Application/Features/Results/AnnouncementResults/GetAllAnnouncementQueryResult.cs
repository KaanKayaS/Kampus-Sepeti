using KampüsSepeti.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Results.AnnouncementResults
{
    public class GetAllAnnouncementQueryResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UserName{ get; set; }
        public string LocationName { get; set; }
        public int commentCount { get; set; }
        public string UserImage { get; set; }
    }
}
