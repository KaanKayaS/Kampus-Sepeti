using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public LocationDto Location { get; set; }
        public UserDto User { get; set; }
    }
}
