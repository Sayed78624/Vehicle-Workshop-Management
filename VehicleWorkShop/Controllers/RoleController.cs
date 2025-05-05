using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRole role;
        public RoleController(IRole role)
        {
            this.role = role;
        }
        public async Task<IActionResult> Index()
        {
            var rolelist = await role.GetAll();
            return View(rolelist);
        }
        [HttpGet]
        public IActionResult Create()
        {
            RoleVM rolevm = new RoleVM();
            return View(rolevm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                var result = await role.Create(roleVM);
                return RedirectToAction("Index");
            }
            return View(roleVM);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var allrole = await role.GetAll();
            var roleVM = allrole.FirstOrDefault(s => s.RoleId == id);

            if (roleVM == null)
            {
                return NotFound();
            }
            return View(roleVM);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await role.Delete(id);

            if (result is OkResult)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await role.GetById(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                var updaterole = await role.Update(roleVM);
                return RedirectToAction("Index");
            }
            return View(roleVM);
        }
    }
}
