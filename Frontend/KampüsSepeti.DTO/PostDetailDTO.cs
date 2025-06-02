using KampüsSepeti.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.DTO
{
    public class PostDetailDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string? BrandName { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
        public int LocationId { get; set; }
        public int StatusId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public LocationDto Location { get; set; }
        public StatusDto Status { get; set; }
        public UserDto User { get; set; }
        public CategoryDto Category { get; set; }
    }
}
