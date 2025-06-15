using System.ComponentModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.Service.Repository;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplier supplier;
        private readonly IMapper mapper;
        public SupplierController(ISupplier supplier, IMapper mapper)
        {
            this.supplier = supplier;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(string searchTerm = "", int page = 1)
        {
            int pageSize = 7;
            var data = await supplier.GetAll();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                data = data.Where(c => c.SupplierName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
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
            SupplierVM supplierVM = new SupplierVM();
            return View(supplierVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SupplierVM supplierVM)
        {
            if (ModelState.IsValid)
            {
                var data = await supplier.Create(supplierVM);
                return RedirectToAction("Index");
            }
            return View(supplierVM);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var allSuppliers = await supplier.GetAll();
            var supplierVM = allSuppliers.FirstOrDefault(s => s.SupplierId == id);

            if (supplierVM == null)
            {
                return NotFound();
            }
            return View(supplierVM);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await supplier.Delete(id);

            if (result is OkResult)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await supplier.GetById(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SupplierVM supplierVM)
        {
            if (ModelState.IsValid)
            {
                var updatesupplier = await supplier.Update(supplierVM);
                return RedirectToAction("Index");
            }
            return View(supplierVM);
        }
    }
}
