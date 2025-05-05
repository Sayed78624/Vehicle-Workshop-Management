using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index()
        {
            var storelist = await store.GetAll();
            return View(storelist);
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
