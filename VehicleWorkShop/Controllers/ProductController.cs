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

        public ProductController(IProduct product, ICategory category)
        {
            this.product = product;
            this.category = category;
        }

        public async Task<IActionResult> Index()
        {
            var productList = await product.GetAll();
            return View(productList);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await category.GetAllCategories(), "CategoryId", "Name");
            ProductVM productVM = new ProductVM();
            return View(productVM);
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

            ViewBag.Categories = new SelectList(await category.GetAllCategories(), "CategoryId", "Name");

            return View(productVM);
        }


    }

}
