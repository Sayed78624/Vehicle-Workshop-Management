using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VehicleWorkShop.Data;

namespace VehicleWorkShop.Controllers
{
    public class DashboardController : Controller
    {
        private readonly WorkShopDbContext db;
        public DashboardController(WorkShopDbContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Dashboard()
        {

            var lowStockItems = await db.Stocks
                  .Include(s => s.Product)
                  .GroupBy(s => new { s.ProductId, s.Product.ProductName })
                  .Select(g => new
                  {
                      ProductName = g.Key.ProductName,
                      Quantity = g.Sum(s => s.Quantity)
                  })
                  .Where(x => x.Quantity <= 5)
                  .ToListAsync();

            ViewBag.LowStockChart = JsonConvert.SerializeObject(lowStockItems);


            //Model Wise Product Calculation
            //var modelwithproduct = await (from p in db.Products
            //                              join m in db.VehicleModels
            //                              on p.ModelId equals m.ModelId
            //                              group p by m.ModelName into g
            //                              select new
            //                              {
            //                                  ModelName = g.Key,
            //                                  ProductCount =g.Count()
            //                              }).ToListAsync();
            //ViewBag.ModelNames = modelwithproduct.Select(m => m.ModelName).ToList();
            //ViewBag.ProductCounts = modelwithproduct.Select(p => p.ProductCount).ToList();

            //var totalProducts = await db.Products.CountAsync();
            //var totalStores = await db.Stores.CountAsync();      

            var totalTransfers = await db.Transfers.CountAsync();
            var approvedTransfers = await db.Transfers.CountAsync(x => x.IsApprove);       
            var purchaseproduct = await db.Purchases.CountAsync();
            var approvepurchase = await db.Purchases.CountAsync(x => x.IsApprove);
            var salesproduct = await db.Sales.CountAsync();
            var approvesales = await db.Sales.CountAsync(s => s.IsApprove);


            //ViewBag.TotalProducts = totalProducts;
            //ViewBag.TotalStores = totalStores;
            ViewBag.TotalTransfers = totalTransfers;
            ViewBag.ApprovedTransfers = approvedTransfers;
            ViewBag.TotalPurchase = purchaseproduct;
            ViewBag.ApprovedPurchase = approvepurchase;
            ViewBag.TotalSales = salesproduct;
            ViewBag.ApprovedSales = approvesales;
            return View();
        }
    }
}
