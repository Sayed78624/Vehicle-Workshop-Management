using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStore store;
        public StoreController(IStore store)
        {
            this.store = store;
        }
        public async Task<IActionResult> Index(string searchTerm = "", int page = 1)
        {
            int pageSize = 7;
            var data = await store.GetAll();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                data = data.Where(c => c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            int totalItems = data.Count();
            var pagedData = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchTerm = searchTerm;

            return View(pagedData);
        }
        [HttpGet]
        public IActionResult Create()
        {
            StoreVM storeVM = new StoreVM();
            return View(storeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(StoreVM storeVM)
        {
            if (ModelState.IsValid)
            {
                var result = await store.Create(storeVM);
                return RedirectToAction("Index");
            }
            return View(storeVM);
        }
    }
}
