using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using KampüsSepeti.DTO;
using Microsoft.AspNetCore.Authorization;

namespace KampüsSepeti.UI.Controllers
{
    [Authorize]
    public class MyAnnouncementController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const int PageSize = 6;

        public MyAnnouncementController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
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

                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync($"http://localhost:5000/api/Announcement/GetAnnouncementByUserId?id={userId}");

                if (response.IsSuccessStatusCode)
                {
                    var announcements = await response.Content.ReadFromJsonAsync<List<AnnouncementDTO>>();
                    
                    // Sayfalama işlemleri
                    var totalItems = announcements.Count;
                    var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
                    var currentPage = Math.Max(1, Math.Min(page, totalPages));
                    
                    var pagedAnnouncements = announcements
                        .Skip((currentPage - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();

                    ViewBag.CurrentPage = currentPage;
                    ViewBag.TotalPages = totalPages;
                    ViewBag.HasPreviousPage = currentPage > 1;
                    ViewBag.HasNextPage = currentPage < totalPages;

                    return View(pagedAnnouncements);
                }
                else
                {
                    TempData["Error"] = "Duyurular yüklenirken bir hata oluştu.";
                    return View(new List<AnnouncementDTO>());
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Bir hata oluştu: " + ex.Message;
                return View(new List<AnnouncementDTO>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Index", "Login");
                }

                var client = _httpClientFactory.CreateClient();
                
                // Önce duyurunun kullanıcıya ait olup olmadığını kontrol et
                var userAnnouncementsResponse = await client.GetAsync($"http://localhost:5000/api/Announcement/GetAnnouncementByUserId?id={userId}");
                if (!userAnnouncementsResponse.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Duyuru bilgileri yüklenirken bir hata oluştu.";
                    return RedirectToAction("Index");
                }

                var userAnnouncements = await userAnnouncementsResponse.Content.ReadFromJsonAsync<List<AnnouncementDTO>>();
                if (!userAnnouncements.Any(a => a.Id == id))
                {
                    TempData["Error"] = "Bu duyuruyu düzenleme yetkiniz yok.";
                    return RedirectToAction("Index");
                }
                
                // Duyuru bilgilerini getir
                var announcementResponse = await client.GetAsync($"http://localhost:5000/api/Announcement/GetAnnouncementById?id={id}");
                
                // Konum listesini getir
                var locationsResponse = await client.GetAsync("http://localhost:5000/api/Locations/GetAllLocation");

                if (announcementResponse.IsSuccessStatusCode && locationsResponse.IsSuccessStatusCode)
                {
                    var announcement = await announcementResponse.Content.ReadFromJsonAsync<EditAnnouncementDto>();
                    var locations = await locationsResponse.Content.ReadFromJsonAsync<List<LocationDto>>();
                    
                    ViewBag.Locations = locations;
                    return View(announcement);
                }
                else
                {
                    TempData["Error"] = "Bilgiler yüklenirken bir hata oluştu.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Bir hata oluştu: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAnnouncementDto announcement)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Index", "Login");
                }

                // Önce duyurunun kullanıcıya ait olup olmadığını kontrol et
                var client = _httpClientFactory.CreateClient();
                var userAnnouncementsResponse = await client.GetAsync($"http://localhost:5000/api/Announcement/GetAnnouncementByUserId?id={userId}");
                if (!userAnnouncementsResponse.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Duyuru bilgileri yüklenirken bir hata oluştu.";
                    return RedirectToAction("Index");
                }

                var userAnnouncements = await userAnnouncementsResponse.Content.ReadFromJsonAsync<List<AnnouncementDTO>>();
                if (!userAnnouncements.Any(a => a.Id == announcement.Id))
                {
                    TempData["Error"] = "Bu duyuruyu düzenleme yetkiniz yok.";
                    return RedirectToAction("Index");
                }

                var response = await client.PutAsJsonAsync("http://localhost:5000/api/Announcement", announcement);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Duyuru başarıyla güncellendi.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Duyuru güncellenirken bir hata oluştu.";
                    return View(announcement);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Bir hata oluştu: " + ex.Message;
                return View(announcement);
            }
        }
    }
} 