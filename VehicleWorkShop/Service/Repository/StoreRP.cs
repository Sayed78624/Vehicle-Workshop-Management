using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleWorkShop.Data;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Repository
{
    public class StoreRP: IStore
    {
        private readonly WorkShopDbContext db;
        private readonly IMapper mapper;
        public StoreRP(WorkShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<List<StoreVM>> GetAll()
        {
            var list = await db.Stores.ToListAsync();
            var storelist = mapper.Map<List<StoreVM>>(list);
            return storelist;
        }
        public async Task<IActionResult> Create(StoreVM storeVM)
        {
            try
            {
                Store store = new Store
                {
                    StoreId = storeVM.StoreId,
                    Name = storeVM.Name,
                };
                db.Stores.Add(store);
                await db.SaveChangesAsync();
                return new JsonResult(new { success = true, message = "Workshop created successfully!" });
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                return new JsonResult(ErrorMessage);
            }
        }

        public async Task<IList<Store>> GetAllStores()
        {
           return await db.Stores.ToListAsync();
        }

        public async Task<List<StoreProductReportVM>> GetProductReport()
        {
            var report = await db.Stocks
                .Where(x => x.Quantity > 0)
                .GroupBy(x => new { x.StoreId, x.ProductId })
                .Select(g => new
                {
                    StoreId = g.Key.StoreId,
                    ProductId = g.Key.ProductId,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .Join(db.Stores, s => s.StoreId, store => store.StoreId, (s, store) => new { s, store })
                .Join(db.Products, sp => sp.s.ProductId, product => product.ProductId, (sp, product) => new
                {
                    sp.s.StoreId,
                    sp.store.Name,
                    product.ProductId,
                    product.ProductName,
                    product.PartNo,
                    sp.s.Quantity
                })
                .GroupBy(x => new { x.StoreId, x.Name })
                .Select(g => new StoreProductReportVM
                {
                    StoreId = g.Key.StoreId,
                    StoreName = g.Key.Name,
                    Products = g.Select(p => new ProductStockVM
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                        PartsNo = p.PartNo,
                        Quantity = p.Quantity
                    }).ToList()
                })
                .ToListAsync();

            return report;
        }



    }

}

