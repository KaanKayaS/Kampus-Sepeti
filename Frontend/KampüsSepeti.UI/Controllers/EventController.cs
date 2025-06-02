using KampüsSepeti.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace KampüsSepeti.UI.Controllers
{
    public class EventController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string EventApiUrl = "https://backend.etkinlik.io/api/v2/events?format_ids=&category_ids=&venue_ids=&city_ids=&skip=&take=2000";
        private const string CityApiUrl = "https://backend.etkinlik.io/api/v2/cities";
        private const string CategoryApiUrl = "https://backend.etkinlik.io/api/v2/categories";
        private const string Token = "9a1198d8681c8e8c53e3d4c9efa3f04d";

        public EventController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int? cityId = null, int? categoryId = null, string searchTerm = null)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Etkinlik-Token", Token);

            // Şehirleri çek
            var citiesResponse = await client.GetAsync(CityApiUrl);
            var cities = new List<City>();
            if (citiesResponse.IsSuccessStatusCode)
            {
                var citiesContent = await citiesResponse.Content.ReadAsStringAsync();
                try
                {
                    cities = JsonSerializer.Deserialize<List<City>>(citiesContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                catch (JsonException ex)
                {
                    Console.WriteLine("Cities JSON Error: " + ex.Message);
                }
            }

            // Kategorileri çek
            var categoriesResponse = await client.GetAsync(CategoryApiUrl);
            var categories = new List<Category>();
            if (categoriesResponse.IsSuccessStatusCode)
            {
                var categoriesContent = await categoriesResponse.Content.ReadAsStringAsync();
                try
                {
                    categories = JsonSerializer.Deserialize<List<Category>>(categoriesContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                catch (JsonException ex)
                {
                    Console.WriteLine("Categories JSON Error: " + ex.Message);
                }
            }

            // Etkinlikleri çek
            var eventsResponse = await client.GetAsync(EventApiUrl);
            var events = new List<Event>();
            if (eventsResponse.IsSuccessStatusCode)
            {
                var content = await eventsResponse.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<EventApiResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                events = apiResponse?.items ?? new List<Event>();

                // Filtreleme
                if (cityId.HasValue)
                {
                    events = events.Where(e => e.Venue?.City?.Id == cityId).ToList();
                }

                if (categoryId.HasValue)
                {
                    events = events.Where(e => e.Category?.Id == categoryId).ToList();
                }

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    events = events.Where(e => e.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                }
            }

            var viewModel = new EventFilterViewModel
            {
                Cities = cities,
                Categories = categories,
                Events = events,
                SelectedCityId = cityId,
                SelectedCategoryId = categoryId,
                SearchTerm = searchTerm
            };

            return View(viewModel);
        }
    }
}
