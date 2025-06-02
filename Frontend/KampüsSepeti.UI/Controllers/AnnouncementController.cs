using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using KampüsSepeti.Application.Features.Results.AnnouncementResults;
using KampüsSepeti.DTO;
using System.Text;
using System.Security.Claims;
using SendGrid;
using KampüsSepeti.Application.Features.Results.CommentResults;

namespace KampüsSepeti.UI.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly HttpClient _httpClient;

        public AnnouncementController()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            try
                {
                var response = await _httpClient.GetAsync("http://localhost:5000/api/Announcement");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var allAnnouncements = JsonConvert.DeserializeObject<List<GetAllAnnouncementQueryResult>>(content);
                    
                    // Popüler duyurular için tüm duyuruları ViewBag'e ekle
                    ViewBag.AllAnnouncements = allAnnouncements;
                    
                    // Sayfalama
                    const int pageSize = 10;
                    var totalCount = allAnnouncements.Count;
                    var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
                    
                    // Geçerli sayfa numarasını kontrol et
                    page = Math.Max(1, Math.Min(page, totalPages));
                    
                    // Sayfalı verileri al
                    var announcements = allAnnouncements
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    ViewBag.CurrentPage = page;
                    ViewBag.TotalPages = totalPages;
                    ViewBag.TotalCount = totalCount;
                    
                    return View(announcements);
                }
            }
            catch (System.Exception ex)
            {
                // Hata durumunda varsayılan değerler
                ViewBag.CurrentPage = 1;
                ViewBag.TotalPages = 1;
                ViewBag.TotalCount = 0;
                return View(new List<GetAllAnnouncementQueryResult>());
            }

            // Varsayılan değerler
            ViewBag.CurrentPage = 1;
            ViewBag.TotalPages = 1;
            ViewBag.TotalCount = 0;
            return View(new List<GetAllAnnouncementQueryResult>());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://localhost:5000/api/Locations/GetAllLocation");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var locations = JsonConvert.DeserializeObject<List<LocationDto>>(content);
                    ViewBag.Locations = locations;
                }

                ViewBag.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            catch (System.Exception ex)
            {
                ViewBag.Locations = new List<LocationDto>();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAnnouncementDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    
                    var response = await _httpClient.PostAsync("http://localhost:5000/api/Announcement", content);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError("", "Duyuru oluşturulurken bir hata oluştu.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"http://localhost:5000/api/Announcement/GetAnnouncementById?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var announcement = JsonConvert.DeserializeObject<GetAnnouncementByIdQueryResult>(content);
                    return View(announcement);
                }
            }
            catch (System.Exception ex)
            {
                // Hata durumunda ana sayfaya yönlendir
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

       
    }
}
