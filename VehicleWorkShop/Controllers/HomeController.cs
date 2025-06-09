using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleWorkShop.Data;
using VehicleWorkShop.Service.Interface;

namespace VehicleWorkShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStore store;
        private readonly WorkShopDbContext db;
        public HomeController (IStore store, WorkShopDbContext db)
        {
            this.store = store;
            this.db = db;
        }
        public IActionResult Home()
        {
            ViewData["Title"] = "Home";

            return View();

   
        }
        public async Task<IActionResult> AllStoreReport()
        {
            var reportData = await store.GetProductReport();
            return View(reportData);
        }
    }
}
