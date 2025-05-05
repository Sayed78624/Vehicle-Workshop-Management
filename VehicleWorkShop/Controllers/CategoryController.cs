using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.Service.Repository;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategory category;
        public CategoryController(ICategory category)
        {
            this.category = category;
        }
        public async Task<IActionResult> Index()
        {
            var catlist = await category.GetAll();
            return View(catlist);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CategoryVM categoryVM = new CategoryVM();
            return View(categoryVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                var result = await category.Create(categoryVM);
                return RedirectToAction("Index");
            }
            return View(categoryVM);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var allSuppliers = await category.GetAll();
            var supplierVM = allSuppliers.FirstOrDefault(s => s.CategoryId == id);

            if (supplierVM == null)
            {
                return NotFound();
            }
            return View(supplierVM);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await category.Delete(id);

            if (result is OkResult)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await category.GetById(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryVM supplierVM)
        {
            if (ModelState.IsValid)
            {
                var updatesupplier = await category.Update(supplierVM);
                return RedirectToAction("Index");
            }
            return View(supplierVM);
        }
    }
}
