using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleWorkShop.Data;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Repository
{
    public class SaleRP : ISale
    {
        private readonly WorkShopDbContext db;
        private readonly IMapper mapper;
        public SaleRP(WorkShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Create(SaleVM saleVM)
        {
            try
            {
                var detailList = saleVM.SaleDetailVMs.Select(detail => new SaleDetails
                {
                    SaleDetailsId = detail.SaleDetailsId,
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity,
                    Price = detail.Price,
                    Vat = detail.Vat,
                    SubTotal = (detail.Quantity * detail.Price) + detail.Vat,
                    StoreId = detail.StoreId,
                    SaleId = detail.SaleId,
                    WorkShopId = detail.WorkShopId == 0 ? null: detail.WorkShopId,
                    BayId = detail.BayId == 0 ? null : detail.BayId,
                    LevelId =detail.LevelId == 0 ? null :detail.LevelId,
                    Vin = string.IsNullOrEmpty(detail.Vin) ? null : detail.Vin,
                    RegisterNo =string.IsNullOrEmpty(detail.RegisterNo) ? null : detail.RegisterNo,
                    StartTime =detail.StartTime == TimeSpan.Zero ? null : detail.StartTime,
                    EndTime =detail.EndTime == TimeSpan.Zero ? null :detail.EndTime,

                }).ToList();
                
                var grandTotal = detailList.Sum(d => d.SubTotal);
                Sale sale = new Sale
                {
                    Description = saleVM.Description,
                    CustomerId = saleVM.CustomerId,
                    GrandTotal = grandTotal,
                    IsApprove = saleVM.IsApprove,
                };
                db.Sales.Add(sale);
                await db.SaveChangesAsync();
                foreach(var detail in  detailList)
                {
                    detail.SaleId = sale.SaleId;
                }
                db.SalesDetails.AddRange(detailList);
                await db.SaveChangesAsync();
                if (sale.IsApprove)
                {
                    foreach (var detail in detailList)
                    {
                        var existingLedger = await db.Ledgers.FirstOrDefaultAsync(x =>
                            x.ProductId == detail.ProductId && x.StoreId == detail.StoreId);

                        if (existingLedger != null)
                        {
                            existingLedger.Quantity -= detail.Quantity;
                            db.Ledgers.Update(existingLedger);
                        }
                        else
                        {
                            Ledger newLedger = new Ledger
                            {
                                ProductId = detail.ProductId,
                                Quantity = detail.Quantity,
                                Price = detail.Price,
                                InventoryTypeId = saleVM.InventoryTypeId ?? 3,
                                StockTypeId = saleVM.StockTypeId ?? 2,
                                StoreId = detail.StoreId,
                                UserId = 2
                            };

                            db.Ledgers.Add(newLedger);
                        }
                    }
                    db.SaveChanges();
                }
                foreach (var detail in detailList)
                {
                    var existingstock = await db.Stocks.FirstOrDefaultAsync(a => a.ProductId == detail.ProductId && a.StoreId == detail.StoreId);
                    if (existingstock != null)
                    {
                        existingstock.Quantity -= detail.Quantity;
                    }
                    else
                    {
                        Stock newstock = new Stock
                        {
                            ProductId = detail.ProductId,
                            Quantity = detail.Quantity,
                            StoreId = detail.StoreId,
                        };
                        db.Stocks.Add(newstock);
                    }
                }
                db.SaveChanges();
                return new OkResult();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return new BadRequestResult();
            }
        }

        public async Task<List<SaleVM>> GetAll()
        {
            var list = await db.Sales.Include(s => s.SaleDetails).ToListAsync();
            var saleList = mapper.Map<List<SaleVM>>(list);
            return saleList;
        }
        public async Task<IActionResult> Delete(int id)
        {
            var sale = await db.Sales
                .Include(p => p.SaleDetails)
                .FirstOrDefaultAsync(p => p.SaleId == id);

            if (sale != null)
            {
                db.SalesDetails.RemoveRange(sale.SaleDetails);

                db.Sales.Remove(sale);

                await db.SaveChangesAsync();

                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }
    }
}
