using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Controllers
{
    public class ModelController : Controller
    {
        private readonly IVehicleModel vehicleModel;
        public ModelController(IVehicleModel vehicleModel)
        {
            this.vehicleModel = vehicleModel;
        }
        public async Task<IActionResult> Index()
        {
            var rolelist = await vehicleModel.GetAll();
            return View(rolelist);
        }
        [HttpGet]
        public IActionResult Create()
        {
            VehicleModelVM vehicleModelVM = new VehicleModelVM();
            return View(vehicleModelVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(VehicleModelVM vehicleModelVM)
        {
            if (ModelState.IsValid)
            {
                var result = await vehicleModel.Create(vehicleModelVM);
                return RedirectToAction("Index");
            }
            return View(vehicleModelVM);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var allmodel = await vehicleModel.GetAll();
            var model = allmodel.FirstOrDefault(s => s.ModelId == id);

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await vehicleModel.Delete(id);

            if (result is OkResult)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await vehicleModel.GetById(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(VehicleModelVM vehicleModelVM)
        {
            if (ModelState.IsValid)
            {
                var updatemodel = await vehicleModel.Update(vehicleModelVM);
                return RedirectToAction("Index");
            }
            return View(vehicleModelVM);
        }
    }
}
