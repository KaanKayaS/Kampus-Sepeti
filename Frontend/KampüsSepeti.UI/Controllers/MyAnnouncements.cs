using Microsoft.AspNetCore.Mvc;

namespace KampüsSepeti.UI.Controllers
{
    public class MyAnnouncements : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
