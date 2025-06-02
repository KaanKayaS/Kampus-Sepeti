using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using KampüsSepeti.UI.Hubs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using KampüsSepeti.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KampüsSepeti.UI.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;

            return View();
        }
    }
} 