using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using KampüsSepeti.Application.Features.Commands.LoginCommands;
using KampüsSepeti.DTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace KampüsSepeti.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Main");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginCommand loginCommand)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var content = new StringContent(JsonSerializer.Serialize(loginCommand), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:5000/api/Auth/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var tokenModel = JsonSerializer.Deserialize<LoginDto>(jsonData, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    if (tokenModel != null && !string.IsNullOrEmpty(tokenModel.Token))
                    {
                        // Önce eski cookie'yi temizle
                        Response.Cookies.Delete("AccessToken");

                        // Yeni token'ı kaydet
                        Response.Cookies.Append("AccessToken", tokenModel.Token, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = false, // Development'da false, production'da true olmalı
                            SameSite = SameSiteMode.Lax,
                            Expires = tokenModel.Expiration
                        });

                        var handler = new JwtSecurityTokenHandler();
                        var token = handler.ReadJwtToken(tokenModel.Token);
                        var claims = token.Claims.ToList();
                        claims.Add(new Claim("accessToken", tokenModel.Token));
                        
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProps = new AuthenticationProperties
                        {
                            ExpiresUtc = tokenModel.Expiration,
                            IsPersistent = true
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProps);

                        return RedirectToAction("Index", "Main");
                    }
                }

                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                return View(loginCommand);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Bir hata oluştu: " + ex.Message);
                return View(loginCommand);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveToken([FromBody] LoginDto loginDto)
        {
            if (loginDto != null && !string.IsNullOrEmpty(loginDto.Token))
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(loginDto.Token);
                var claims = token.Claims.ToList();

                claims.Add(new Claim("accessToken", loginDto.Token));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProps = new AuthenticationProperties
                {
                    ExpiresUtc = loginDto.Expiration,
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProps);

                return Ok();
            }

            return BadRequest("Token bilgileri geçersiz");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                var email = User.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
                
                if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(email))
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var revokeCommand = new { Email = email };
                    var content = new StringContent(JsonSerializer.Serialize(revokeCommand), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("http://localhost:5000/api/Auth/Revoke", content);
                    
                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Revoke token error: {error}");
                    }
                }

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                Console.WriteLine($"Logout error: {ex.Message}");
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
