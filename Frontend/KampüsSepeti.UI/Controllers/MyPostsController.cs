using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using KampüsSepeti.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SendGrid;

namespace KampüsSepeti.UI.Controllers
{
    [Authorize]
    public class MyPostsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const int PageSize = 6;

        public MyPostsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpDelete("RemovePost")]
        public async Task<IActionResult> RemovePost(int id)
        {
            Console.WriteLine($"Silme işlemi için gelen ID: {id}");  // Debug için ekledik

            var client = _httpClientFactory.CreateClient();
            var deletedPost = await client.DeleteAsync($"http://localhost:5000/api/Posts/RemovePost?id={id}");

            if (deletedPost.IsSuccessStatusCode)
            {
                return Ok("Post başarıyla silindi.");
            }
            else
            {
                return BadRequest("Post silinemedi.");
            }
        }




        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Index", "Login");
                }

                Console.WriteLine($"Fetching posts for user ID: {userId}"); // Debug için

                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync($"http://localhost:5000/api/Posts/GetAllPostByUserId?id={userId}");

                Console.WriteLine($"API Response Status: {response.StatusCode}"); // Debug için

                if (response.IsSuccessStatusCode)
                {
                    var posts = await response.Content.ReadFromJsonAsync<List<PostDto>>();
                    Console.WriteLine($"Retrieved {posts?.Count} posts"); // Debug için

                    // Pagination hesaplamaları
                    var totalPosts = posts?.Count ?? 0;
                    var totalPages = (int)Math.Ceiling(totalPosts / (double)PageSize);
                    var currentPagePosts = posts?
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize)
                        .ToList() ?? new List<PostDto>();

                    ViewBag.CurrentPage = page;
                    ViewBag.TotalPages = totalPages;
                    ViewBag.HasPreviousPage = page > 1;
                    ViewBag.HasNextPage = page < totalPages;
                    ViewBag.TotalPosts = totalPosts;

                    return View(currentPagePosts);
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Error: {error}"); // Debug için
                    TempData["Error"] = "İlanlar yüklenirken bir hata oluştu.";
                }

                return View(new List<PostDto>());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}"); // Debug için
                TempData["Error"] = "Bir hata oluştu: " + ex.Message;
                return View(new List<PostDto>());
            }
        }
    }
}
