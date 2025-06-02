using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Results.FavoriteResults
{
    public class GetFavoritePostsByUserIdQueryResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
