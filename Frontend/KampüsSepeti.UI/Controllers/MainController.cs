using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using KampüsSepeti.DTO;
using System.Linq;
using System;
using System.Text.Json.Serialization;
using KampüsSepeti.Application.Features.Results.PostResults;
using KampüsSepeti.Application.Features.Results.CategoryResults;
using System.Text.Json;
using Newtonsoft.Json;
using LocationDto = KampüsSepeti.Application.DTOs.LocationDto;

namespace KampüsSepeti.UI.Controllers
{
    [Authorize]
    public class MainController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const int PageSize = 12; // Sayfa başına post sayısı

        public MainController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            await LoadCategories(); // Kategorileri yükle
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("http://localhost:5000/api/Posts/GetAllPost");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var allPosts = System.Text.Json.JsonSerializer.Deserialize<List<PostDto>>(content, options);

                    // Pagination hesaplamaları
                    var totalPosts = allPosts.Count;
                    var totalPages = (int)Math.Ceiling(totalPosts / (double)PageSize);
                    var currentPagePosts = allPosts
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();

                    ViewBag.CurrentPage = page;
                    ViewBag.TotalPages = totalPages;
                    ViewBag.HasPreviousPage = page > 1;
                    ViewBag.HasNextPage = page < totalPages;
                    ViewBag.TotalPosts = totalPosts; // Toplam ilan sayısı
                    ViewBag.LocationName = "Tüm Şehirler"; // Lokasyon adı

                    return View(currentPagePosts);
                }

                return View(new List<PostDto>());
            }
            catch
            {
                return View(new List<PostDto>());
            }
        }

        public async Task<IActionResult> GetPostsByLocation(int locationId, int page = 1)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                
                // Önce lokasyon adını al
                var locationResponse = await client.GetAsync($"http://localhost:5000/api/Locations/GetLocationById?id={locationId}");
                string locationName = "Seçili Şehir";
                if (locationResponse.IsSuccessStatusCode)
                {
                    var locationContent = await locationResponse.Content.ReadAsStringAsync();
                    var location = System.Text.Json.JsonSerializer.Deserialize<LocationDto>(locationContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    locationName = location.Name;
                }

                var response = await client.GetAsync($"http://localhost:5000/api/Posts/GetPostByLocation?id={locationId}");


                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var allPosts = System.Text.Json.JsonSerializer.Deserialize<List<PostDto>>(content, options);

                    // Pagination hesaplamaları
                    var totalPosts = allPosts.Count;
                    var totalPages = (int)Math.Ceiling(totalPosts / (double)PageSize);
                    var currentPagePosts = allPosts
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();

                    ViewBag.CurrentPage = page;
                    ViewBag.TotalPages = totalPages;
                    ViewBag.HasPreviousPage = page > 1;
                    ViewBag.HasNextPage = page < totalPages;
                    ViewBag.LocationId = locationId;
                    ViewBag.TotalPosts = totalPosts; // Toplam ilan sayısı
                    ViewBag.LocationName = locationName; // Lokasyon adı

                    return View("Index", currentPagePosts);
                }

                return View("Index", new List<PostDto>());
            }
            catch
            {
                return View("Index", new List<PostDto>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> FilterPosts(string searchTitle, decimal? minPrice, decimal? maxPrice, int page = 1)
        {
            try
            {
                // Önce tüm ilanları al
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("http://localhost:5000/api/Posts/GetAllPost");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var allPosts = JsonConvert.DeserializeObject<List<PostDto>>(content);

                    // LINQ ile filtreleme işlemleri
                    var query = allPosts.AsQueryable();

                    Console.WriteLine($"Başlangıç ilan sayısı: {query.Count()}");

                    // Başlığa göre filtreleme
                    if (!string.IsNullOrWhiteSpace(searchTitle))
                    {
                        var searchTerms = searchTitle.ToLower().Split(' ');
                        query = query.Where(x => searchTerms.Any(term => x.Title.ToLower().Contains(term)));
                        Console.WriteLine($"Başlık filtresinden sonra: {query.Count()}");
                    }

                    // Fiyat aralığına göre filtreleme
                    if (minPrice.HasValue && minPrice.Value > 0)
                    {
                        query = query.Where(x => x.Price >= minPrice.Value);
                        Console.WriteLine($"Min fiyat filtresinden sonra: {query.Count()}");
                    }

                    if (maxPrice.HasValue && maxPrice.Value > 0)
                    {
                        query = query.Where(x => x.Price <= maxPrice.Value);
                        Console.WriteLine($"Max fiyat filtresinden sonra: {query.Count()}");
                    }

                    // Filtrelenmiş sonuçları al
                    var filteredPosts = query.ToList();
                    Console.WriteLine($"Toplam filtrelenmiş ilan: {filteredPosts.Count}");

                    // Sonuç yoksa
                    if (!filteredPosts.Any())
                    {
                        SetViewBagForNoResults(searchTitle, minPrice, maxPrice);
                        return View("Index", new List<PostDto>());
                    }

                    // Sayfalama
                    var totalPosts = filteredPosts.Count;
                    var totalPages = (int)Math.Ceiling(totalPosts / (double)PageSize);
                    var pagedPosts = filteredPosts
                        .OrderByDescending(x => x.CreatedDate)
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();

                    // ViewBag'i ayarla
                    SetViewBagForResults(page, totalPages, totalPosts, searchTitle, minPrice, maxPrice);

                    return View("Index", pagedPosts);
                }

                SetViewBagForNoResults(searchTitle, minPrice, maxPrice);
                return View("Index", new List<PostDto>());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in FilterPosts: {ex.Message}");
                SetViewBagForNoResults(searchTitle, minPrice, maxPrice);
                return View("Index", new List<PostDto>());
            }
        }

        private void SetViewBagForNoResults(string searchTitle, decimal? minPrice, decimal? maxPrice)
        {
            ViewBag.CurrentPage = 1;
            ViewBag.TotalPages = 0;
            ViewBag.HasPreviousPage = false;
            ViewBag.HasNextPage = false;
            ViewBag.TotalPosts = 0;
            ViewBag.SearchTitle = searchTitle;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.LocationName = "Tüm Şehirler";
            ViewBag.NoResults = true;
        }

        private void SetViewBagForResults(int page, int totalPages, int totalPosts, string searchTitle, decimal? minPrice, decimal? maxPrice)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.HasPreviousPage = page > 1;
            ViewBag.HasNextPage = page < totalPages;
            ViewBag.TotalPosts = totalPosts;
            ViewBag.SearchTitle = searchTitle;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.LocationName = "Tüm Şehirler";
            ViewBag.NoResults = false;
        }

        private async Task LoadCategories()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5000/api/Categories");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<GetAllCategoryQueryResult>>(jsonData);
                ViewBag.Categories = categories;
            }
        }

        [HttpGet]
        public async Task<IActionResult> PostDetail(int id)
        {
            ViewBag.postid = id;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5000/api/Posts/GetPostById?id=" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<PostDetailDTO>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
