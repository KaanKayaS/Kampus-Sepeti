using MediatR;
using KampüsSepeti.Application.Features.Results.PostResults;

namespace Core.Application.Features.Queries.PostQueries
{
    public class FilterPostsQuery : IRequest<FilterPostsQueryResult>
    {
        public string? SearchTitle { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? CategoryId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
} 