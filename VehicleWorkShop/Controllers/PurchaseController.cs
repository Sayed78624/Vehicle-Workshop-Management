using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IPurchase _purchase;
        private readonly ISupplier _supplier;
        private readonly IProduct _product;
        private readonly IStore _store;
        private readonly IVehicleModel _vehicleModel;

        public PurchaseController(IPurchase purchase, ISupplier supplier, IProduct product, IStore store, IVehicleModel vehicleModel)
        {
            _purchase = purchase;
            _supplier = supplier;
            _product = product;
            _store = store;
            _vehicleModel = vehicleModel;
        }

        [HttpGet]
       
        public async Task<IActionResult> Approve(int id)
        {
            var purchase = await _purchase.GetById(id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase); 
        }


        [HttpPost]
    
        public async Task<IActionResult> ConfirmApprove(int id)
        {
            try
            {
                var purchase = await _purchase.GetById(id);
                if (purchase != null)
                {
                    await _purchase.Approve(purchase);
                }

                TempData["Success"] = "Purchase approved successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Approval failed: " + ex.Message;
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            try
            {
                var supplierList = await _supplier.GetAllSupplier() ?? new List<Supplier>();
                var productList = await _product.GetAllProducts() ?? new List<Product>();
                var storeList = await _store.GetAllStores() ?? new List<Store>();
                var modelList = await _vehicleModel.GetAllModels() ?? new List<VehicleModel>();

                var suppliers = new List<SelectListItem>();
                foreach (var item in supplierList)
                {
                    var selec = new SelectListItem();
                    selec.Value = item.SupplierId.ToString();
                    selec.Text = item.SupplierName;
                    suppliers.Add(selec);
                }
                var models = new List<SelectListItem>();
                foreach (var item in modelList)
                {
                    var selec = new SelectListItem();
                    selec.Value = item.ModelId.ToString();
                    selec.Text = item.ModelName;
                    models.Add(selec);
                }
                var products = new List<SelectListItem>();
                foreach (var item in productList)
                {
                    var selec = new SelectListItem();
                    selec.Value = item.ProductId.ToString();
                    selec.Text = item.ProductName;
                    products.Add(selec);
                }
                var stores = new List<SelectListItem>();
                foreach (var item in storeList)
                {
                    var selec = new SelectListItem();
                    selec.Value = item.StoreId.ToString();
                    selec.Text = item.Name;
                    stores.Add(selec);
                }

                var purchaseVM = new PurchaseVM();
                if (id != null)
                {
                    var purchase = await _purchase.GetById((int)id);
                    if (purchase != null)
                    {
                        purchaseVM = purchase;
                    }
                }
                purchaseVM.Suppliers = suppliers;
                purchaseVM.Products = products;
                purchaseVM.Stores = stores;
                //purchaseVM.Suppliers = suppliers;
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
        public async Task<IActionResult> Create(PurchaseVM purchaseVM)
        {
            if (ModelState.IsValid)
            {
                if (purchaseVM.IsApprove)
                {
                    ViewBag.ErrorMessage = "Model is not allowed to modify.";
                }
                else
                {
                    try
                    {
                        var purchase = await _purchase.CreateMaster(purchaseVM);
                        var purchaseDetailVM = new PurchaseDetailVM();
                        purchaseDetailVM.Quantity = purchaseVM.Quantity;
                        purchaseDetailVM.Price = purchaseVM.Price;
                        purchaseDetailVM.PurchaseId = purchase.PurchaseId;
                        purchaseDetailVM.ProductId = purchaseVM.ProductId;
                        purchaseDetailVM.StoreId = purchaseVM.StoreId;
                        purchaseDetailVM.SubTotal = purchaseVM.SubTotal;
                        purchaseDetailVM.Vat = purchaseVM.Vat;
                        purchaseDetailVM.ModelId = purchaseVM.ModelId;
                        await _purchase.CreateDetail(purchaseDetailVM);
                        return RedirectToAction("Create", new { id = purchase.PurchaseId });
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

            if (purchaseVM.PurchaseId > 0)
            {
                purchaseVM = await _purchase.GetById(purchaseVM.PurchaseId);
            }

            var supplierList = await _supplier.GetAllSupplier() ?? new List<Supplier>();
            var productList = await _product.GetAllProducts() ?? new List<Product>();
            var storeList = await _store.GetAllStores() ?? new List<Store>();
            var modelList = await _vehicleModel.GetAllModels() ?? new List<VehicleModel>();

            var suppliers = new List<SelectListItem>();
            foreach (var item in supplierList)
            {
                var selec = new SelectListItem();
                selec.Value = item.SupplierId.ToString();
                selec.Text = item.SupplierName;
                suppliers.Add(selec);
            }
            var models = new List<SelectListItem>();
            foreach (var item in modelList)
            {
                var selec = new SelectListItem();
                selec.Value = item.ModelId.ToString();
                selec.Text = item.ModelName;
                models.Add(selec);
            }
            var products = new List<SelectListItem>();
            foreach (var item in productList)
            {
                var selec = new SelectListItem();
                selec.Value = item.ProductId.ToString();
                selec.Text = item.ProductName;
                products.Add(selec);
            }
            var stores = new List<SelectListItem>();
            foreach (var item in storeList)
            {
                var selec = new SelectListItem();
                selec.Value = item.StoreId.ToString();
                selec.Text = item.Name;
                stores.Add(selec);
            }
            purchaseVM.Suppliers = suppliers;
            purchaseVM.Products = products;
            purchaseVM.Stores = stores;
            purchaseVM.Suppliers = suppliers;
            purchaseVM.Models = models;

            return View(purchaseVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDetail(PurchaseDetailVM purchaseDetailVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _purchase.CreateDetail(purchaseDetailVM);
                    return RedirectToAction("Create", new { id = purchaseDetailVM.PurchaseId });
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"Error occurred while saving the purchase: {ex.Message}";
                }
            }
            return RedirectToAction("Create", new { id = purchaseDetailVM.PurchaseId });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveDetail(int id)
        {
            try
            {
                var purchaseDetailVM = await _purchase.RemoveDetail(id);
                return RedirectToAction("Create", new { id = purchaseDetailVM.PurchaseId });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error occurred while saving the purchase: {ex.Message}";
            }
            return RedirectToAction("Create", new { id = 0 });
        }

        public async Task<IActionResult> Index()
        {
            var datalist = await _purchase.GetAll();
            return View(datalist);
        }

        public async Task<IActionResult> Invoice(int id)
        {
            var invoice = await _purchase.GetInvoice(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }

        public async Task<IActionResult> DownloadInvoice(int id)
        {
            var invoice = await _purchase.GetInvoice(id);

            if (invoice == null)
                return NotFound();

            var viewData = new ViewDataDictionary<PurchaseInvoice>(
                new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = invoice  
            };

            viewData["From"] = "pdf";

            var pdfView = new ViewAsPdf("~/Views/Purchase/Invoice.cshtml", invoice)
            {
                FileName = $"PurchaseInvoice_{id}.pdf",
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait,
                ViewData = viewData,
                PageMargins = new Margins(10, 10, 10, 10) 
            };


            return pdfView;
        }


    }
}
