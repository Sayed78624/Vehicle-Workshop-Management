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

        public async Task<IActionResult> Index(string? searchTerm)
        {
            var productList = await product.GetAll(searchTerm);
            ViewBag.CurrentFilter = searchTerm; 
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

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var allproducts = await product.GetAllProducts();
            var productvm = allproducts.FirstOrDefault(a => a.ProductId == id);
            if (productvm != null)
            {
                return View(productvm);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await product.Delete(id);
            if (result is OkResult)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var products = await product.GetById(id);
            if(products == null) return NotFound();

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
            var productvm = new ProductVM
            {
                ProductId = products.ProductId,
                ProductName = products.ProductName,
                Price = products.Price,
                PartNo = products.PartNo,
                Description = products.Description,
                CategoryId = products.CategoryId,
                ModelId = products.ModelId,
                ImageName = products.ImageName,
                Categories = categories,
                VehicleModel = models
            };
            return View(productvm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                await product.Update(productVM);
                return RedirectToAction("Index");
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
