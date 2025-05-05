using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleWorkShop.Data;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Repository
{
    public class PurchaseRP : IPurchase
    {
        private readonly WorkShopDbContext db;
        private readonly IMapper mapper;

        public PurchaseRP(WorkShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }


        public async Task<List<Purchase>> GetAll()
        {
            var purchases = await db.Purchases
                .Include(p => p.PurchaseDetails) // Ensure details are loaded
                .ToListAsync();
            return purchases;

            //var purchaseVMs = mapper.Map<List<PurchaseVM>>(purchases); // Correct mapping
            //return purchaseVMs;
        }



        public async Task<IActionResult> Create(Purchase purchase)
        {

            await db.Purchases.AddAsync(purchase);
            await db.SaveChangesAsync();
            return new OkResult();
            //try
            //{
            //    Purchase purchase = new Purchase
            //    {
            //        PurchaseId = purchaseVM.PurchaseId,
            //        Description = purchaseVM.Description,
            //        SupplierId = purchaseVM.SupplierId,
            //        GrandTotal = purchaseVM.GrandTotal,
            //        IsApprove = purchaseVM.IsApprove,
            //    };

            //    db.Purchases.Add(purchase);
            //    await db.SaveChangesAsync();

            //    foreach (var detail in purchaseVM.PurchaseDetails)
            //    {
            //        var purchaseDetail = new PurchaseDetail
            //        {
            //            PurchaseDetailId = detail.PurchaseDetailId,
            //            PurchaseId = purchase.PurchaseId,
            //            ProductId = detail.ProductId,
            //            Quantity = detail.Quantity,
            //            Price = detail.Price,
            //            Vat = detail.Vat,
            //            SubTotal = (detail.Quantity * detail.Price) + detail.Vat,
            //            StoreId = detail.StoreId
            //        };
            //        db.PurchasesDetails.Add(purchaseDetail);
            //    }

            //    await db.SaveChangesAsync();

            //    return new OkResult();
            //}
            //catch (Exception ex)
            //{
            //    var ErrorMessage = ex.Message;
            //    return new BadRequestResult();
            //}
        }
    }
    }



//public async Task<IActionResult> Create(PurchaseVM purchaseVM)
//{
//    if (!purchaseVM.PurchaseDetails.Any())
//    {
//        return new BadRequestObjectResult("Please add at least one product.");
//    }

//    foreach (var detail in purchaseVM.PurchaseDetails)
//    {
//        if (detail.ProductId <= 0)
//            return new BadRequestObjectResult("Product is required in each row.");
//        if (detail.Price <= 0)
//            return new BadRequestObjectResult("Price must be greater than 0.");
//        if (detail.Quantity <= 0)
//            return new BadRequestObjectResult("Quantity must be at least 1.");
//    }

//    if (!purchaseVM.InventoryTypeId.HasValue || !purchaseVM.StockTypeId.HasValue)
//    {
//        return new BadRequestObjectResult("Inventory Type and Stock Type are required.");
//    }

//    purchaseVM.GrandTotal = purchaseVM.PurchaseDetails
//        .Sum(x => (x.Price * x.Quantity) + x.Vat);

//    using var transaction = await db.Database.BeginTransactionAsync();
//    try
//    {
//        var purchase = new Purchase
//        {
//            PurchaseId = purchaseVM.PurchaseId,
//            Description = purchaseVM.Description,
//            SupplierId = purchaseVM.SupplierId,
//            GrandTotal = purchaseVM.GrandTotal,
//            IsApprove = purchaseVM.IsApprove
//        };

//        db.Purchases.Add(purchase);
//        await db.SaveChangesAsync();

//        foreach (var detail in purchaseVM.PurchaseDetails)
//        {
//            var purchaseDetail = new PurchaseDetail
//            {
//                PurchaseDetailId = detail.PurchaseDetailId,
//                PurchaseId = purchase.PurchaseId,
//                ProductId = detail.ProductId,
//                Quantity = detail.Quantity,
//                Price = detail.Price,
//                Vat = detail.Vat,
//                SubTotal = (detail.Quantity * detail.Price) + detail.Vat,
//                StoreId = detail.StoreId
//            };

//            db.PurchasesDetails.Add(purchaseDetail);
//        }

//        await db.SaveChangesAsync();

//        if (purchaseVM.IsApprove)
//        {
//            var purchaseDetails = await db.PurchasesDetails
//                .Where(x => x.PurchaseId == purchase.PurchaseId)
//                .ToListAsync();

//            foreach (var detail in purchaseDetails)
//            {
//                var ledger = new Ledger
//                {
//                    LedgerId = detail.StoreId,
//                    ProductId = detail.ProductId,
//                    Quantity = detail.Quantity,
//                    InventoryTypeId = 1,  // Set InventoryType = 1
//                    StockTypeId = 1,      // Set StockType = 1
//                    UserId = null            // Assume a default UserId (or pass the actual user id)
//                };

//                db.Ledgers.Add(ledger);
//                await db.SaveChangesAsync();

//                detail.StoreId = ledger.LedgerId;
//                db.PurchasesDetails.Update(detail);

//                if (ledger.InventoryTypeId == 1 && ledger.StockTypeId == 1)
//                {
//                    var stock = await db.Stocks
//                        .FirstOrDefaultAsync(s => s.ProductId == detail.ProductId && s.S == 1);

//                    if (stock == null)
//                    {
//                        stock = new Stock
//                        {
//                            StockId = detail.ProductId,
//                            ProductId = detail.ProductId,
//                            LedgerId = ledger.LedgerId,
//                            Quantity = detail.Quantity,
//                            StockTypeId = 1
//                        };
//                        db.Stocks.Add(stock);
//                    }
//                    else
//                    {
//                        stock.Quantity += detail.Quantity;
//                        db.Stocks.Update(stock);
//                    }
//                }
//            }

//            await db.SaveChangesAsync();
//        }

//        await transaction.CommitAsync();
//        return new OkObjectResult(new { message = purchaseVM.IsApprove ? "Purchase created and approved." : "Purchase created (pending approval)." });
//    }
//    catch (Exception ex)
//    {
//        await transaction.RollbackAsync();
//        return new JsonResult(new { error = ex.Message });
//    }
//}