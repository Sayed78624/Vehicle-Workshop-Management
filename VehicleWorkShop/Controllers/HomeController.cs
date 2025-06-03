using Microsoft.AspNetCore.Mvc;

namespace VehicleWorkShop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            ViewData["Title"] = "Home";
            return View();
        }
    }
}
