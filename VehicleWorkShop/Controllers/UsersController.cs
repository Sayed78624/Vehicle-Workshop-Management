using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUser user;
        private readonly IMapper mapper;
        public UsersController(IUser user, IMapper mapper)
        {
            this.user = user;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var list = await user.GetAll();
            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            UserVM userVM = new UserVM();
            return View(userVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                var data = await user.Create(userVM);
                return RedirectToAction("Index");
            }
            return View(userVM);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var allusers = await user.GetAll();
            var userVM = allusers.FirstOrDefault(s => s.UserId == id);

            if (userVM == null)
            {
                return NotFound();
            }
            return View(userVM);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await user.Delete(id);

            if (result is OkResult)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await user.GetById(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                var updateuser = await user.Update(userVM);
                return RedirectToAction("Index");
            }
            return View(userVM);
        }
    }
}
