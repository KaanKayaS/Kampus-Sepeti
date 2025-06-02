using KampüsSepeti.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;

namespace KampüsSepeti.UI.Controllers
{
    public class DefaultController : Controller
    {   
        public IActionResult Index()
        {
           return View();
        }
    }
}
