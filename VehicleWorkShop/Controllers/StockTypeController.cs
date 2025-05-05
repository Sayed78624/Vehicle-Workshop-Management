using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.Service.Repository;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Controllers
{
    public class StockTypeController : Controller
    {
        private readonly IStockType stockType;
        public StockTypeController(IStockType stockType)
        {
            this.stockType = stockType;
        }
        public async Task<IActionResult> Index()
        {
            var typelist = await stockType.GetAll();
            return View(typelist);
        }
        [HttpGet]
        public IActionResult Create()
        {
            StockTypeVM stockTypeVM = new StockTypeVM();
            return View(stockTypeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(StockTypeVM stockTypeVM)
        {
            if (ModelState.IsValid)
            {
                var result = await stockType.Create(stockTypeVM);
                return RedirectToAction("Index");
            }
            return View(stockType);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var allSuppliers = await stockType.GetAll();
            var supplierVM = allSuppliers.FirstOrDefault(s => s.StockTypeId == id);

            if (supplierVM == null)
            {
                return NotFound();
            }
            return View(supplierVM);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await stockType.Delete(id);

            if (result is OkResult)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await stockType.GetById(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(StockTypeVM supplierVM)
        {
            if (ModelState.IsValid)
            {
                var updatesupplier = await stockType.Update(supplierVM);
                return RedirectToAction("Index");
            }
            return View(supplierVM);
        }
    }
}
