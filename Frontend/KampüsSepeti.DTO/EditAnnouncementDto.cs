using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.DTO
{
    public class EditAnnouncementDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
    }
}
