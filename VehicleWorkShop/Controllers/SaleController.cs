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
        private readonly ISale _sale;
        private readonly ICustomer _customer;
        private readonly IProduct _product;
        private readonly IStore _store;
        private readonly IWorkShop _workShop;
        private readonly IVehicleModel _vehicleModel;
        public SaleController(ISale sale, IProduct product, IStore store, ICustomer customer, IWorkShop workShop, IVehicleModel vehicleModel)
        {
            _sale = sale;
            _product = product;
            _store = store;
            _customer = customer;
            _workShop = workShop;
            _vehicleModel = vehicleModel;
        }

        public async Task<IActionResult> Index()
        {
            var datalist = await _sale.GetAll();
            return View(datalist);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            try
            {
                var workshopList = await _workShop.GetAllWorkshops() ?? new List<WorkShop>();
                var productList = await _product.GetAllProducts() ?? new List<Product>();
                var storeList = await _store.GetAllStores() ?? new List<Store>();
                var customerList = await _customer.GetAllCustomers() ?? new List<Customer>();
                var modelList = await _vehicleModel.GetAllModels() ?? new List<VehicleModel>();

                var customers = new List<SelectListItem>();
                foreach (var item in customerList)
                {
                    var selec = new SelectListItem();
                    selec.Value = item.CustomerId.ToString();
                    selec.Text = item.Name;
                    customers.Add(selec);
                }

                var products = new List<SelectListItem>();
                foreach (var item in productList)
                {
                    var selec = new SelectListItem();
                    selec.Value = item.ProductId.ToString();
                    selec.Text = item.ProductName;
                    products.Add(selec);
                }
                var models = new List<SelectListItem>();
                foreach (var item in modelList)
                {
                    var selec = new SelectListItem();
                    selec.Value = item.ModelId.ToString();
                    selec.Text = item.ModelName;
                    models.Add(selec);
                }
                var stores = new List<SelectListItem>();
                foreach (var item in storeList)
                {
                    var selec = new SelectListItem();
                    selec.Value = item.StoreId.ToString();
                    selec.Text = item.Name;
                    stores.Add(selec);
                }

                var workshops = new List<SelectListItem>();
                foreach (var item in workshopList)
                {
                    var selec = new SelectListItem();
                    selec.Value = item.WorkShopId.ToString();
                    selec.Text = item.WorkShopName;
                    workshops.Add(selec);
                }
                var purchaseVM = new SaleVM();
                if (id != null)
                {
                    var purchase = await _sale.GetById((int)id);
                    if (purchase != null)
                    {
                        purchaseVM = purchase;
                    }
                }
                purchaseVM.WorkShopes = workshops;
                purchaseVM.Products = products;
                purchaseVM.Stores = stores;
                purchaseVM.Customers = customers;
                purchaseVM.Models = models;
                return View(purchaseVM);
            }
            catch (Exception ex)
            {
                return Content($"Error: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaleVM saleVM)
        {
            if (ModelState.IsValid)
            {
                if (saleVM.IsApprove)
                {
                    ViewBag.ErrorMessage = "Model is not allowed to modify.";
                }
                else
                {
                    try
                    {
                        var purchase = await _sale.CreateMaster(saleVM);
                        var purchaseDetailVM = new SaleDetailVM();
                        purchaseDetailVM.Quantity = saleVM.Quantity;
                        purchaseDetailVM.Price = saleVM.Price;
                        purchaseDetailVM.SaleId = purchase.SaleId;
                        purchaseDetailVM.ProductId = saleVM.ProductId;
                        purchaseDetailVM.StoreId = saleVM.StoreId;
                        purchaseDetailVM.SubTotal = saleVM.SubTotal;
                        purchaseDetailVM.Vat = saleVM.Vat;
                        purchaseDetailVM.WorkShopId = saleVM.WorkShopId;
                        purchaseDetailVM.BayId = saleVM.BayId;
                        purchaseDetailVM.LevelId = saleVM.LevelId;
                        purchaseDetailVM.Vin = saleVM.Vin;
                        purchaseDetailVM.RegisterNo = saleVM.RegisterNo;
                        purchaseDetailVM.StartTime = saleVM.StartTime;
                        purchaseDetailVM.EndTime = saleVM.EndTime;
                        purchaseDetailVM.CustomerId = saleVM.CustomerId;
                        purchaseDetailVM.ModelId = saleVM.ModelId;
                        await _sale.CreateDetail(purchaseDetailVM);
                        return RedirectToAction("Create", new { id = purchase.SaleId });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = $"Error occurred while saving the purchase: {ex.Message}";
                    }
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Model not valid.";
            }

            if (saleVM.SaleId > 0)
            {
                saleVM = await _sale.GetById(saleVM.SaleId);
            }

            var workshopList = await _workShop.GetAllWorkshops() ?? new List<WorkShop>();
            var productList = await _product.GetAllProducts() ?? new List<Product>();
            var storeList = await _store.GetAllStores() ?? new List<Store>();
            var customerList = await _customer.GetAllCustomers() ?? new List<Customer>();
            var modelList = await _vehicleModel.GetAllModels() ?? new List<VehicleModel>();

            var workshops = new List<SelectListItem>();
            foreach (var item in workshopList)
            {
                var selec = new SelectListItem();
                selec.Value = item.WorkShopId.ToString();
                selec.Text = item.WorkShopName;
                workshops.Add(selec);
            }
            var products = new List<SelectListItem>();
            foreach (var item in productList)
            {
                var selec = new SelectListItem();
                selec.Value = item.ProductId.ToString();
                selec.Text = item.ProductName;
                products.Add(selec);
            }
            var models = new List<SelectListItem>();
            foreach (var item in modelList)
            {
                var selec = new SelectListItem();
                selec.Value = item.ModelId.ToString();
                selec.Text = item.ModelName;
                models.Add(selec);
            }
            var stores = new List<SelectListItem>();
            foreach (var item in storeList)
            {
                var selec = new SelectListItem();
                selec.Value = item.StoreId.ToString();
                selec.Text = item.Name;
                stores.Add(selec);
            }
            var customers = new List<SelectListItem>();
            foreach (var item in customerList)
            {
                var selec = new SelectListItem();
                selec.Value = item.CustomerId.ToString();
                selec.Text = item.Name;
                customers.Add(selec);
            }
            saleVM.WorkShopes = workshops;
            saleVM.Products = products;
            saleVM.Stores = stores;
            saleVM.Customers = customers;
            saleVM.Models = models;

            return View(saleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDetail(SaleDetailVM saleDetailVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _sale.CreateDetail(saleDetailVM);
                    return RedirectToAction("Create", new { id = saleDetailVM.SaleId });
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"Error occurred while saving the purchase: {ex.Message}";
                }
            }
            return RedirectToAction("Create", new { id = saleDetailVM.SaleId });
        }

        [HttpGet]
        public async Task<IActionResult> Approve(int? id)
        {
            try
            {

                var saleVM = new SaleVM();
                if (id != null)
                {
                    var purchase = await _sale.GetById((int)id);
                    if (purchase != null)
                    {
                        saleVM = purchase;
                        await _sale.Approve(saleVM);
                    }
                }


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content($"Error: {ex.Message}");
            }
        }

    }
}
