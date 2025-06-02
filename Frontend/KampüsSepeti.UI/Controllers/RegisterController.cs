using KampüsSepeti.Application.Features.Commands.RegisterCommands;
using KampüsSepeti.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace KampüsSepeti.UI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegisterController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Main");
            }
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5000/api/Locations/GetAllLocation");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<LocationDto>>(jsonData);
                List<SelectListItem> locationValues = (from x in values
                                                    select new SelectListItem
                                                    {
                                                        Text = x.Name,
                                                        Value = x.Id.ToString()
                                                    }).ToList();
                ViewBag.Location = locationValues;
            }

            var responseMessage2 = await client.GetAsync("http://localhost:5000/api/Universities");
            if (responseMessage2.IsSuccessStatusCode)
            {
                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
                var values2 = JsonConvert.DeserializeObject<List<UniversityDto>>(jsonData2);
                List<SelectListItem> universityValues = (from x in values2
                                                       select new SelectListItem
                                                       {
                                                           Text = x.Name,
                                                           Value = x.Id.ToString()
                                                       }).ToList();
                ViewBag.University = universityValues;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterCommand registerCommand)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                return View(registerCommand);
            }

            try
            {
                var client = httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(registerCommand);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("http://localhost:5000/api/Auth/Register", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Login");
                }
                
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                
                try
                {
                    var errors = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(responseContent);
                    if (errors != null)
                    {
                        foreach (var error in errors)
                        {
                            foreach (var message in error.Value)
                            {
                                ModelState.AddModelError(error.Key, message);
                            }
                        }
                    }
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, responseContent);
                }

                await LoadDropdowns();
                return View(registerCommand);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Bir hata oluştu: " + ex.Message);
                await LoadDropdowns();
                return View(registerCommand);
            }
        }

        // Dropdown listeleri için yardımcı metod
        private async Task LoadDropdowns()
        {
            var client = httpClientFactory.CreateClient();
            
            var locationResponse = await client.GetAsync("http://localhost:5000/api/Locations/GetAllLocation");
            if (locationResponse.IsSuccessStatusCode)
            {
                var locationData = await locationResponse.Content.ReadAsStringAsync();
                var locations = JsonConvert.DeserializeObject<List<LocationDto>>(locationData);
                ViewBag.Location = locations.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            }

            var universityResponse = await client.GetAsync("http://localhost:5000/api/Universities");
            if (universityResponse.IsSuccessStatusCode)
            {
                var universityData = await universityResponse.Content.ReadAsStringAsync();
                var universities = JsonConvert.DeserializeObject<List<UniversityDto>>(universityData);
                ViewBag.University = universities.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            }
        }
    }
}
