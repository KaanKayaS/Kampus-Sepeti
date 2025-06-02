using KampüsSepeti.Application.DTOs;
using System;
using System.Collections.Generic;

namespace KampüsSepeti.Application.Features.Results.PostResults
{
    public class FilterPostsQueryResult
    {
        public List<PostItemDto> Items { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class PostItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
