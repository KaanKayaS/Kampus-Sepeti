using KampüsSepeti.Application.DTOs;
using KampüsSepeti.Application.Features.Results.LocationResults;
using KampüsSepeti.Domain.Entities;
using KampüsSepeti.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;

namespace KampüsSepeti.UI.ViewComponents.DefaultViewComponents
{
    public class _NavbarDefaultComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory httpClientFactory;

        public _NavbarDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                // Lokasyonları getir
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

                var userid = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int userId = int.Parse(userid);

                var responseMessageimagePath = await client.GetAsync($"http://localhost:5000/api/MyProfile?id={userId}");

                if (responseMessageimagePath.IsSuccessStatusCode)
                {
                    var jsonDataImage = await responseMessageimagePath.Content.ReadAsStringAsync();
                    ViewBag.ImagePath = jsonDataImage;
                }
              

                // Kullanıcı bilgilerini getir
                if (User.Identity.IsAuthenticated)
                {
                    var token = HttpContext.Request.Cookies["AccessToken"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        var userResponse = await client.GetAsync("http://localhost:5000/api/Users/GetCurrentUser");
                        if (userResponse.IsSuccessStatusCode)
                        {
                            var userData = await userResponse.Content.ReadAsStringAsync();
                            var user = JsonConvert.DeserializeObject<UserDto>(userData);
                            ViewBag.UserFullName = user.FullName;
                        }
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
