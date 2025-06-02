using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using KampüsSepeti.Application.Features.Commands.PostCommands;
using System.Text.Json;
using KampüsSepeti.Application.DTOs;
using KampüsSepeti.DTO;
using CategoryDto = KampüsSepeti.Application.DTOs.CategoryDto;
using StatusDto = KampüsSepeti.Application.DTOs.StatusDto;
using LocationDto = KampüsSepeti.Application.DTOs.LocationDto;

namespace KampüsSepeti.UI.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PostController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> CreatePost()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                // Lokasyonları getir
                var locationResponse = await client.GetAsync("http://localhost:5000/api/Locations/GetAllLocation");
                if (locationResponse.IsSuccessStatusCode)
                {
                    var locations = await locationResponse.Content.ReadFromJsonAsync<List<DTO.LocationDto>>();
                    ViewBag.Locations = locations;
                }

                // Durumları getir
                var statusResponse = await client.GetAsync("http://localhost:5000/api/Statuses");
                if (statusResponse.IsSuccessStatusCode)
                {
                    var jsonString = await statusResponse.Content.ReadAsStringAsync();
                    Console.WriteLine($"Status API Response: {jsonString}"); // Debug için API yanıtını görelim

                    try 
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var statuses = JsonSerializer.Deserialize<List<DTO.StatusDto>>(jsonString, options);
                        
                        // Null check ve debug
                        if (statuses != null)
                        {
                            foreach (var status in statuses)
                            {
                                Console.WriteLine($"Status: Id={status.Id}, Name={status.Name}");
                            }
                        }
                        
                        ViewBag.Statuses = statuses;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Status deserialization error: {ex.Message}");
                        ViewBag.Statuses = new List<DTO.StatusDto>();
                    }
                }

                // Kategorileri getir
                var categoryResponse = await client.GetAsync("http://localhost:5000/api/Categories");
                if (categoryResponse.IsSuccessStatusCode)
                {
                    var categories = await categoryResponse.Content.ReadFromJsonAsync<List<DTO.CategoryDto>>();
                    ViewBag.Categories = categories;
                }

                return View(new CreatePostCommand());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Bir hata oluştu: " + ex.Message;
                return RedirectToAction("Index", "Main");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostCommand command, IFormFile image)
        {
            try
            {
                // Debug için form verilerini yazdır
                Console.WriteLine("Form verileri:");
                Console.WriteLine($"Image file: {(image != null ? $"Size: {image.Length} bytes" : "null")}");
                Console.WriteLine($"Command: {JsonSerializer.Serialize(command)}");

                if (!ModelState.IsValid)
                {
                    Console.WriteLine("Model state is invalid");
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                    }
                    return View(command);
                }

                // Kullanıcı ID'sini al
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    TempData["Error"] = "Kullanıcı bilgisi bulunamadı.";
                    return RedirectToAction("Index", "Main");
                }

                // Resmi kaydet
                if (image != null && image.Length > 0)
                {
                    try
                    {
                        // wwwroot/uploads klasörünü kullan
                        var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                        if (!Directory.Exists(uploadsPath))
                        {
                            Directory.CreateDirectory(uploadsPath);
                        }

                        // Benzersiz dosya adı oluştur
                        var fileName = $"{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                        var filePath = Path.Combine(uploadsPath, fileName);
                        
                        Console.WriteLine($"Saving image to: {filePath}");
                        
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        
                        // URL yolunu düzgün format ile kaydet
                        command.Image = $"/uploads/{fileName}";
                        Console.WriteLine($"Image path saved: {command.Image}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error saving image: {ex.Message}");
                        Console.WriteLine($"Stack trace: {ex.StackTrace}");
                        // Hata durumunda varsayılan resmi kullan
                        command.Image = "/uploads/default.jpg";
                    }
                }
                else
                {
                    // Resim yüklenmemişse varsayılan resmi kullan
                    command.Image = "/uploads/default.jpg";
                    Console.WriteLine("No image uploaded, using default image");
                }

                command.UserId = int.Parse(userId);

                // API isteği öncesi command içeriğini logla
                Console.WriteLine("Sending command to API:");
                Console.WriteLine($"Title: {command.Title}");
                Console.WriteLine($"Description: {command.Description}");
                Console.WriteLine($"Price: {command.Price}");
                Console.WriteLine($"BrandName: {command.BrandName}");
                Console.WriteLine($"Image: {command.Image}");
                Console.WriteLine($"UserId: {command.UserId}");
                Console.WriteLine($"LocationId: {command.LocationId}");
                Console.WriteLine($"StatusId: {command.StatusId}");
                Console.WriteLine($"CategoryId: {command.CategoryId}");

                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync("http://localhost:5000/api/Posts/CreatePost", command);

                // API yanıtını detaylı logla
                Console.WriteLine($"API Response Status: {response.StatusCode}");
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Response Content: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "İlan başarıyla oluşturuldu.";
                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    TempData["Error"] = $"İlan oluşturulurken bir hata oluştu: {responseContent}";
                    return View(command);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in CreatePost: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                TempData["Error"] = "Bir hata oluştu: " + ex.Message;
                return View(command);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                
                // Post bilgilerini getir
                var postResponse = await client.GetAsync($"http://localhost:5000/api/Posts/GetPostById?id={id}");
                if (!postResponse.IsSuccessStatusCode)
                {
                    TempData["Error"] = "İlan bilgileri alınamadı.";
                    return RedirectToAction("Index", "MyPosts");
                }
                var post = await postResponse.Content.ReadFromJsonAsync<PostDetailDTO>();

                // Post null check
                if (post == null)
                {
                    TempData["Error"] = "İlan bulunamadı.";
                    return RedirectToAction("Index", "MyPosts");
                }

                // Kullanıcının postun sahibi olup olmadığını kontrol et
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId) || post.UserId.ToString() != userId)
                {
                    TempData["Error"] = "Bu ilanı düzenleme yetkiniz bulunmamaktadır.";
                    return RedirectToAction("Index", "MyPosts");
                }

                // Debug için post verilerini yazdır
                Console.WriteLine($"Post data:");
                Console.WriteLine($"Id: {post.Id}");
                Console.WriteLine($"Title: {post.Title}");
                Console.WriteLine($"LocationId: {post.LocationId}");
                Console.WriteLine($"CategoryId: {post.CategoryId}");
                Console.WriteLine($"StatusId: {post.StatusId}");

                // API çağrıları
                var locationsResponse = await client.GetAsync("http://localhost:5000/api/Locations/GetAllLocation");
                var categoriesResponse = await client.GetAsync("http://localhost:5000/api/Categories");
                var statusesResponse = await client.GetAsync("http://localhost:5000/api/Statuses");

                if (locationsResponse.IsSuccessStatusCode)
                {
                    var locations = await locationsResponse.Content.ReadFromJsonAsync<List<LocationDto>>();
                    ViewBag.Locations = locations;
                }
                
                if (categoriesResponse.IsSuccessStatusCode)
                {
                    var categories = await categoriesResponse.Content.ReadFromJsonAsync<List<CategoryDto>>();
                    ViewBag.Categories = categories;
                }
                
                if (statusesResponse.IsSuccessStatusCode)
                {
                    var statuses = await statusesResponse.Content.ReadFromJsonAsync<List<StatusDto>>();
                    ViewBag.Statuses = statuses;
                }

                return View("EditPost", post);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Edit action error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                TempData["Error"] = "İlan bilgileri yüklenirken bir hata oluştu.";
                return RedirectToAction("Index", "MyPosts");
            }
        }
    }
}
