using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.Service.Repository;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISale sale;
        private readonly ICustomer customer;
        private readonly IProduct product;
        private readonly IWorkShop workShop;
        private readonly IStore store;
        public SaleController(ISale sale, ICustomer customer, IProduct product, IWorkShop workShop, IStore store)
        {
            this.sale = sale;
            this.customer = customer;
            this.product = product;
            this.workShop = workShop;
            this.store = store;
        }

        public async Task<IActionResult> Index()
        {
            var result =await sale.GetAll();
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var customers = await customer.GetAllCustomers() ?? new List<Customer>();
            var products = await product.GetAllProducts() ?? new List<Product>();
            var workshops = await workShop.GetAllWorkshops() ?? new List<WorkShop>();
            var stores = await store.GetAllStores() ?? new List<Store>();

            ViewBag.Customers = new SelectList(customers, "CustomerId", "Name");
            ViewBag.Products = new SelectList(products, "ProductId", "PartNo");
            ViewBag.Workshops = new SelectList(workshops, "WorkShopId", "WorkShopName");
            ViewBag.Stores = new SelectList(stores, "StoreId", "Name");

            return View(new SaleVM { SaleDetailVMs = new List<SaleDetailVM> { new SaleDetailVM() } });
        }
        [HttpPost]
        public async Task<IActionResult> Create(SaleVM saleVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await sale.Create(saleVM);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    return new BadRequestResult();
                }
            }

            ViewBag.Customers = new SelectList(await customer.GetAllCustomers(), "CustomerId", "Name");
            ViewBag.Products = new SelectList(await product.GetAllProducts(), "ProductId", "PartNo");
            ViewBag.Workshops = new SelectList(await workShop.GetAllWorkshops(), "WorkShopId", "WorkShopName");
            ViewBag.Stores = new SelectList(await store.GetAllStores(), "StoreId", "Name");

            return View(saleVM);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await sale.Delete(id);
            if (result is OkResult)
            {
                return RedirectToAction(nameof(Index));
            }
            else if (result is BadRequestResult)
            {
                return BadRequest();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
