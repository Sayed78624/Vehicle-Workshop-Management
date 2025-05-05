using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleWorkShop.Data;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.Service.Repository;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IPurchase _purchase;
        private readonly ISupplier _supplier;
        private readonly IProduct _product;
        private readonly IStore _store;
        private readonly WorkShopDbContext _context;
        private readonly IMapper mapper;


        public PurchaseController(IPurchase purchase, ISupplier supplier, IProduct product, IStore store,WorkShopDbContext _context, IMapper mapper)
        {
            _purchase = purchase;
            _supplier = supplier;
            _product = product;
            _store = store;
            this._context = _context;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Suppliers = new SelectList(_context.Suppliers, "SupplierId", "Name");
            ViewBag.Products = new SelectList(_context.Products, "ProductId", "Name");
            ViewBag.Stores = new SelectList(_context.Stores, "StoreId", "Name");

            return View(new PurchaseVM { PurchaseDetails = new List<PurchaseDetailVM> { new PurchaseDetailVM() } });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseVM model)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    await _purchase.Create(purchaseVM);
                    return RedirectToAction("Index", new { id = purchaseVM.PurchaseId });
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"Error occurred while saving the purchase: {ex.Message}";
                }
            }
            ViewBag.Suppliers = new SelectList(await _supplier.GetAllSupplier(), "SupplierId", "SupplierName");
            ViewBag.Products = new SelectList(await _product.GetAllProducts(), "ProductId", "PartNo");
            ViewBag.Stores = new SelectList(await _store.GetAllStores(), "StoreId", "StoreName");


            return View(purchaseVM);
        }

        public async Task<IActionResult> Index()
        {

            var datalist = await _purchase.GetAll();
            return View(datalist);
        }
    }
}
