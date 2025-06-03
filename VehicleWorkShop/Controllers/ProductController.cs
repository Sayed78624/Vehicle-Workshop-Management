using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct product;
        private readonly ICategory category;
        private readonly IVehicleModel vehicleModel;

        public ProductController(IProduct product, ICategory category, IVehicleModel vehicleModel)
        {
            this.product = product;
            this.category = category;
            this.vehicleModel = vehicleModel;
        }

        public async Task<IActionResult> Index()
        {
            var productList = await product.GetAll();
            return View(productList);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categoryList = await category.GetAllCategories() ?? new List<Category>();
            var modelList = await vehicleModel.GetAllModels() ?? new List<VehicleModel>();

            var categories = new List<SelectListItem>();
            foreach (var item in categoryList)
            {
                var selec = new SelectListItem();
                selec.Value = item.CategoryId.ToString();
                selec.Text = item.Name;
                categories.Add(selec);
            }
            var models = new List<SelectListItem>();
            foreach (var item in modelList)
            {
                var selec = new SelectListItem();
                selec.Value = item.ModelId.ToString();
                selec.Text = item.ModelName;
                models.Add(selec);
            }
            var productvm = new ProductVM();
            productvm.Categories = categories;
            productvm.VehicleModel = models;
            return View(productvm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                await product.Create(productVM);
                return RedirectToAction("Index");
            }

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            var categoryList = await category.GetAllCategories() ?? new List<Category>();
            var modelList = await vehicleModel.GetAllModels() ?? new List<VehicleModel>();

            var categories = new List<SelectListItem>();
            foreach (var item in categoryList)
            {
                var selec = new SelectListItem();
                selec.Value = item.CategoryId.ToString();
                selec.Text = item.Name;
                categories.Add(selec);
            }
            var models = new List<SelectListItem>();
            foreach (var item in modelList)
            {
                var selec = new SelectListItem();
                selec.Value = item.ModelId.ToString();
                selec.Text = item.ModelName;
                models.Add(selec);
            }
            return View(productVM);
        }


    }

}
