using KampüsSepeti.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace KampüsSepeti.UI.Controllers
{
    [Route("[controller]")]
    public class MyProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MyProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            try
            {
                var loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                ViewBag.LoggedInUserId = loggedInUserId;
                ViewBag.IsOwnProfile = loggedInUserId == id.ToString();

                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync($"http://localhost:5000/api/Users?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<ProfileDto>();
                    if (data != null)
                    {
                        ViewBag.UserId = id;
                        return View(data);
                    }
                }
                
                return NotFound();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost("UpdateProfilePhoto")]
        public async Task<IActionResult> UpdateProfilePhoto(IFormFile profilePhoto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var client = _httpClientFactory.CreateClient();
                var formData = new MultipartFormDataContent();
                formData.Add(new StreamContent(profilePhoto.OpenReadStream()), "ProfilePhoto", profilePhoto.FileName);

                var response = await client.PutAsync($"http://localhost:5000/api/MyProfile?UserId={userId}", formData);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API'den gelen resim URL'si: {responseContent}");
                    return Ok(new { message = "Profil resmi başarıyla güncellendi", imageUrl = responseContent });
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return BadRequest($"Profil resmi güncellenirken bir hata oluştu: {errorContent}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Bir hata oluştu: " + ex.Message);
            }
        }
    }
}
