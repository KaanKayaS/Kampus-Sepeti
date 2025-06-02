using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using KampüsSepeti.Application.Features.Results.PostResults;
using Core.Application.Features.Queries.PostQueries;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;

namespace Core.Application.Handlers.PostHandlers
{
    public class FilterPostsQueryHandler : IRequestHandler<FilterPostsQuery, FilterPostsQueryResult>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public FilterPostsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FilterPostsQueryResult> Handle(FilterPostsQuery request, CancellationToken cancellationToken)
        {
            // Tüm postları çek
            var posts = await unitOfWork.GetReadRepository<Post>().GetAllAsync(
                predicate: null,
                orderBy: null,
                include: x => x.Include(l => l.Location).Include(c => c.Category)
            );

            Console.WriteLine($"Total posts before filtering: {posts.Count}");

            // LINQ ile filtreleme işlemleri
            var query = posts.AsQueryable();

            // Başlığa göre filtreleme
            if (!string.IsNullOrWhiteSpace(request.SearchTitle))
            {
                var searchTerm = request.SearchTitle.ToLower();
                query = query.Where(x => x.Title.ToLower().Contains(searchTerm));
                Console.WriteLine($"After title filter '{searchTerm}': {query.Count()}");
            }

            // Fiyat aralığına göre filtreleme
            if (request.MinPrice.HasValue)
            {
                query = query.Where(x => x.Price >= request.MinPrice.Value);
                Console.WriteLine($"After min price filter {request.MinPrice}: {query.Count()}");
            }
            if (request.MaxPrice.HasValue)
            {
                query = query.Where(x => x.Price <= request.MaxPrice.Value);
                Console.WriteLine($"After max price filter {request.MaxPrice}: {query.Count()}");
            }

            // Kategoriye göre filtreleme
            if (request.CategoryId.HasValue && request.CategoryId != 0)
            {
                query = query.Where(x => x.CategoryId == request.CategoryId.Value);
                Console.WriteLine($"After category filter {request.CategoryId}: {query.Count()}");
            }

            // Önce tüm filtrelenmiş verileri sırala
            var orderedQuery = query.OrderByDescending(x => x.CreatedDate);

            // Toplam kayıt sayısı
            var totalCount = orderedQuery.Count();
            Console.WriteLine($"Total filtered count: {totalCount}");

            // Sayfalama
            var pagedPosts = orderedQuery
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            Console.WriteLine($"Page {request.Page}, PageSize {request.PageSize}, Records: {pagedPosts.Count}");

            var postDtos = pagedPosts.Select(p => new PostItemDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Price = p.Price,
                Image = p.Image,
                CreatedDate = p.CreatedDate,
                LocationId = p.Location.Id,
                LocationName = p.Location.Name,
                CategoryId = p.Category.Id,
                CategoryName = p.Category.Name
            }).ToList();

            var result = new FilterPostsQueryResult
            {
                Items = postDtos,
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize
            };

            Console.WriteLine($"Returning {result.Items.Count} items");
            return result;
        }
    }
} 