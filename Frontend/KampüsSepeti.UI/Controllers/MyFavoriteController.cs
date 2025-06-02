using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using KampüsSepeti.DTO;

namespace KampüsSepeti.UI.Controllers
{
    [Authorize]
    public class MyFavoriteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const int PageSize = 4; // Her sayfada 4 ilan gösterilecek

        public MyFavoriteController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Favorite(int page = 1)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Index", "Login");
                }

                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync($"http://localhost:5000/api/Posts/GetFavoritePostsByUserId?id={userId}");

                if (response.IsSuccessStatusCode)
                {
                    var favorites = await response.Content.ReadFromJsonAsync<List<PostDto>>();
                    
                    // Pagination için hesaplamalar
                    var totalItems = favorites?.Count ?? 0;
                    var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
                    var currentPageItems = favorites?
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize)
                        .ToList() ?? new List<PostDto>();

                    ViewBag.CurrentPage = page;
                    ViewBag.TotalPages = totalPages;
                    ViewBag.HasPreviousPage = page > 1;
                    ViewBag.HasNextPage = page < totalPages;

                    return View(currentPageItems);
                }

                TempData["Error"] = "Favoriler yüklenirken bir hata oluştu.";
                return View(new List<PostDto>());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Bir hata oluştu: " + ex.Message;
                return View(new List<PostDto>());
            }
        }
    }
}
