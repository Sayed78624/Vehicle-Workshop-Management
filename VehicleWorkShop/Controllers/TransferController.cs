using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Controllers
{
    public class TransferController : Controller
    {
        private readonly ITransfer transfer;
        private readonly IProduct product;
        private readonly IStore store;
        public TransferController(ITransfer transfer, IProduct product, IStore store)
        {
            this.transfer = transfer;
            this.product = product;
            this.store = store;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var productlist = await product.GetAllProducts() ?? new List<Product>();
                var products = productlist.Select(
                    item => new SelectListItem
                    {
                        Value = item.ProductId.ToString(),
                        Text = item.ProductName,
                    }).ToList();

                var sstorelist = await store.GetAllStores() ?? new List<Store>();
                var sstores = sstorelist.Select(item => new SelectListItem
                {
                    Value = item.StoreId.ToString(),
                    Text = item.Name,
                }).ToList();
                var dstorelist = await store.GetAllStores() ?? new List<Store>();
                var dstores = dstorelist.Select(item => new SelectListItem
                {
                    Value = item.StoreId.ToString(),
                    Text = item.Name,
                }).ToList();

                TransferVM transferVM = new TransferVM()
                {
                    Products = products,
                    SourceStores = sstores,
                    DestinationStores = dstores,
                    Details = new List<TransferDetailVM> { new TransferDetailVM() }
                };
                return View(transferVM);    
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return View(message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(TransferVM transferVM)
        {
            if(ModelState.IsValid)
            {
                await transfer.Create(transferVM);
                return RedirectToAction("Index");

            }
            var productlist = await product.GetAllProducts() ?? new List<Product>();
            var products = productlist.Select(
                item => new SelectListItem
                {
                    Value = item.ProductId.ToString(),
                    Text = item.ProductName,
                }).ToList();

            var sstorelist = await store.GetAllStores() ?? new List<Store>();
            var sstores = sstorelist.Select(item => new SelectListItem
            {
                Value = item.StoreId.ToString(),
                Text = item.Name,
            }).ToList();
            var dstorelist = await store.GetAllStores() ?? new List<Store>();
            var dstores = dstorelist.Select(item => new SelectListItem
            {
                Value = item.StoreId.ToString(),
                Text = item.Name,
            }).ToList();
            return View(transferVM);
        }
        public async Task<IActionResult> Index()
        {
            var result = await transfer.GetAll(); 
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await transfer.Delete(id);

            return RedirectToAction("Index");
            return result;
        }


        [HttpGet]
        public async Task<IActionResult> Approve(int? id)
        {
            try
            {
                if (id == null)
                {
                    TempData["Error"] = "Invalid transfer ID.";
                    return RedirectToAction("Index");
                }

                var transferVM = await transfer.GetId(id.Value);
                if (transferVM == null)
                {
                    TempData["Error"] = "Transfer not found.";
                    return RedirectToAction("Index");
                }

                await transfer.Approve(transferVM);

                TempData["Success"] = "Transfer approved successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> AddMore(TransferVM transferVM)
        {
            transferVM.Details.Add(new TransferDetailVM());

            var productlist = await product.GetAllProducts() ?? new List<Product>();
            var products = productlist.Select(
                item => new SelectListItem
                {
                    Value = item.ProductId.ToString(),
                    Text = item.ProductName,
                }).ToList();

            var sstorelist = await store.GetAllStores() ?? new List<Store>();
            var sstores = sstorelist.Select(item => new SelectListItem
            {
                Value = item.StoreId.ToString(),
                Text = item.Name,
            }).ToList();
            var dstorelist = await store.GetAllStores() ?? new List<Store>();
            var dstores = dstorelist.Select(item => new SelectListItem
            {
                Value = item.StoreId.ToString(),
                Text = item.Name,
            }).ToList();

            transferVM.Products = products;
            transferVM.SourceStores = sstores;
            transferVM.DestinationStores = dstores;
            return View("Create", transferVM);
        }


    }
}
