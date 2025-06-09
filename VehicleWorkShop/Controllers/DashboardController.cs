using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            var totalProducts = await db.Products.CountAsync();
            var totalStores = await db.Stores.CountAsync();
            var totalStock = await db.Stocks.SumAsync(x => x.Quantity);
            var totalTransfers = await db.Transfers.CountAsync();
            var approvedTransfers = await db.Transfers.CountAsync(x => x.IsApprove);
            var todayTransfers = await db.Transfers.CountAsync();
            var purchaseproduct = await db.Purchases.CountAsync();
            var todaysale = await db.Sales.CountAsync();

            ViewBag.TotalProducts = totalProducts;
            ViewBag.TotalStores = totalStores;
            ViewBag.TotalStock = totalStock;
            ViewBag.TotalTransfers = totalTransfers;
            ViewBag.ApprovedTransfers = approvedTransfers;
            ViewBag.TodayTransfers = todayTransfers;
            ViewBag.TotalPurchase = purchaseproduct;
            ViewBag.TodaySales = todaysale;
            return View();
        }
    }
}
