using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Controllers
{
    public class InventoryTypeController : Controller
    {
        private readonly IInventoryType inventoryType;
        public InventoryTypeController(IInventoryType inventoryType)
        {
            this.inventoryType = inventoryType;
        }

        public async Task<IActionResult> Index()
        {
            var list = await inventoryType.GetAll();
            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            InventoryTypeVM inventoryTypeVM = new InventoryTypeVM();
            return View(inventoryTypeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(InventoryTypeVM inventoryTypeVM)
        {
            if (ModelState.IsValid)
            {
                await inventoryType.Create(inventoryTypeVM);
                return RedirectToAction("Index");
            }
            return View(inventoryTypeVM);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var allrole = await inventoryType.GetAll();
            var roleVM = allrole.FirstOrDefault(s => s.InventoryTypeId == id);

            if (roleVM == null)
            {
                return NotFound();
            }
            return View(roleVM);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await inventoryType.Delete(id);

            if (result is OkResult)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await inventoryType.GetById(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(InventoryTypeVM roleVM)
        {
            if (!ModelState.IsValid)
            {
                return View(roleVM);
            }

            var existingItem = await inventoryType.GetById(roleVM.InventoryTypeId);
            if (existingItem == null)
            {
                return NotFound();
            }

            var updaterole = await inventoryType.Update(roleVM);
            if (updaterole is OkResult)
            {
                return RedirectToAction("Index");
            }

            return BadRequest();
        }
    }
}
