using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.Service.Repository;
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
        public async Task<IActionResult> Index(string searchTerm = "", int page = 1)
        {
            int pageSize = 5;
            var data = await workShop.WorkShopList();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                data = data.Where(c => c.WorkShopName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
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
            WorkShopVM workShopVM = new WorkShopVM();
            return View(workShopVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(WorkShopVM workshopvm)
        {
            if (ModelState.IsValid)
            {
                var result = await workShop.Create(workshopvm);
                TempData["SuccessMessage"] = "Workshop created successfully!";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Failed to create workshop!";
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
