using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Controllers
{
    public class WorkShopController : Controller
    {
        private readonly IWorkShop workShop;
        public WorkShopController(IWorkShop workShop)
        {
            this.workShop = workShop;
        }
        public async Task<IActionResult> Index()
        {
            var list =await workShop.WorkShopList();
            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            WorkShopVM workShopVM = new WorkShopVM();
            return View(workShopVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(WorkShopVM workshopvm)
        {
            if (ModelState.IsValid)  
            {
                var result = await workShop.Create(workshopvm);  
                return RedirectToAction("Index");
            }
            return View(workshopvm);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var allSuppliers = await workShop.WorkShopList();
            var supplierVM = allSuppliers.FirstOrDefault(s => s.WorkShopId == id);

            if (supplierVM == null)
            {
                return NotFound();
            }
            return View(supplierVM);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await workShop.Delete(id);

            if (result is OkResult)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await workShop.GetById(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(WorkShopVM supplierVM)
        {
            if (ModelState.IsValid)
            {
                var updatesupplier = await workShop.Update(supplierVM);
                return RedirectToAction("Index");
            }
            return View(supplierVM);
        }
    }
}
