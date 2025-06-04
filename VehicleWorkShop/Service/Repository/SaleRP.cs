using AutoMapper;
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

        public async Task<List<SaleVM>> GetAll()
        {
            var list = await (from p in db.Sales
                              join s in db.Customers on p.CustomerId equals s.CustomerId
                              select new SaleVM()
                              {
                                  CustomerId = p.CustomerId,
                                  Description = p.Description,
                                  GrandTotal = p.GrandTotal,
                                  CustomerName = s.Name,
                                  SaleId = p.SaleId,
                                  IsApprove = p.IsApprove,
                              }).ToListAsync();
            return list;
        }


        public async Task<SaleVM> GetById(int id)
        {
            SaleVM oPurchase = null; decimal grandTotal = 0;
            var purchase = await (from p in db.Sales
                                  join s in db.Customers on p.CustomerId equals s.CustomerId
                                  where p.SaleId == id
                                  select new SaleVM()
                                  {
                                      CustomerId = p.CustomerId,
                                      Description = p.Description,
                                      GrandTotal = p.GrandTotal,
                                      CustomerName = s.Name,
                                      SaleId = p.SaleId,
                                      IsApprove = p.IsApprove,
                                  }).FirstOrDefaultAsync();
            if (purchase != null)
            {
                var purchasesDetailsList = await (from pd in db.SalesDetails
                                                  join p in db.Products on pd.ProductId equals p.ProductId
                                                  join s in db.Stores on pd.StoreId equals s.StoreId
                                                  join m in db.VehicleModels on pd.ModelId equals m.ModelId
                                                  where pd.SaleId == id
                                                  select new SaleDetailVM()
                                                  {
                                                      SaleDetailsId = pd.SaleDetailsId,
                                                      Quantity = pd.Quantity,
                                                      Price = pd.Price,
                                                      SaleId = pd.SaleId,
                                                      ProductId = pd.ProductId,
                                                      StoreId = pd.StoreId,
                                                      SubTotal = pd.SubTotal,
                                                      Vat = pd.Vat,
                                                      ProductName = p.ProductName,
                                                      StoreName = s.Name,
                                                      WorkShopId = pd.WorkShopId,
                                                      BayId = pd.BayId,
                                                      LevelId = pd.LevelId,
                                                      Vin = pd.Vin,
                                                      RegisterNo = pd.RegisterNo,
                                                      StartTime = pd.StartTime,
                                                      EndTime = pd.EndTime,
                                                      CustomerId = pd.CustomerId,
                                                      ModelId = pd.ModelId,
                                                      ModelName = m.ModelName
                                                  }).ToListAsync();
                foreach (var item in purchasesDetailsList)
                {
                    grandTotal += item.SubTotal;
                }
                oPurchase = new SaleVM()
                {
                    SaleId = purchase.SaleId,
                    Description = purchase.Description,
                    GrandTotal = grandTotal,
                    IsApprove = purchase.IsApprove,
                    CustomerId = purchase.CustomerId,
                    SaleDetails = purchasesDetailsList
                };
            }
            return oPurchase;
        }
        public async Task<SaleVM> CreateMaster(SaleVM purchaseVM)
        {
            try
            {
                #region Master insert/update
                var purchase = await db.Sales.Where(x => x.SaleId == purchaseVM.SaleId).FirstOrDefaultAsync();
                if (purchase == null)
                {
                    purchase = new Sale
                    {
                        Description = purchaseVM.Description,
                        CustomerId = purchaseVM.CustomerId,
                        GrandTotal = purchaseVM.GrandTotal,
                        IsApprove = purchaseVM.IsApprove,
                    };
                    db.Sales.Add(purchase);
                    await db.SaveChangesAsync();
                    purchaseVM.SaleId = purchase.SaleId;
                }
                else
                {
                    purchase.Description = purchaseVM.Description;
                    purchase.SaleId = purchaseVM.SaleId;
                    purchase.GrandTotal = purchaseVM.GrandTotal;
                    purchase.IsApprove = purchaseVM.IsApprove;
                    await db.SaveChangesAsync();
                }
                #endregion
                return purchaseVM;
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                return purchaseVM;
            }
        }

        public async Task<SaleDetailVM> CreateDetail(SaleDetailVM detail)
        {
            try
            {
                var purchaseDetail = new SaleDetails
                {
                    SaleId = detail.SaleId,
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity,
                    Price = detail.Price,
                    Vat = detail.Vat,
                    SubTotal = (detail.Quantity * detail.Price) + detail.Vat,
                    StoreId = detail.StoreId,
                    WorkShopId = detail.WorkShopId,
                    BayId = detail.BayId,
                    LevelId = detail.LevelId,
                    Vin = detail.Vin,
                    RegisterNo = detail.RegisterNo,
                    StartTime = detail.StartTime,
                    EndTime = detail.EndTime,
                    CustomerId = detail.CustomerId,
                    ModelId = detail.ModelId
                };
                db.SalesDetails.Add(purchaseDetail);
                await db.SaveChangesAsync();
                detail.SaleId = purchaseDetail.SaleId;
                detail.SaleDetailsId = purchaseDetail.SaleDetailsId;
                return detail;
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                return detail;
            }
        }


        public async Task<SaleVM> Approve(SaleVM saleVM)
        {
            try
            {
                #region Insert Ledger + Update Stock
                foreach (var detail in saleVM.SaleDetails)
                {
                    Ledger ledger = new Ledger();
                    ledger.Price = detail.Price;
                    ledger.Quantity = detail.Quantity;
                    ledger.Price = detail.Price;
                    ledger.InventoryTypeId = 2; // Sale
                    ledger.StockTypeId = 1; // Issue
                    ledger.StoreId = detail.StoreId;
                    ledger.ProductId = detail.ProductId;

                    db.Ledgers.Add(ledger);
                    await db.SaveChangesAsync();

                    var oStock = (from x in db.Stocks
                                  where x.ProductId == detail.ProductId && x.StoreId == detail.StoreId
                                  select x).FirstOrDefault();
                    if (oStock != null)
                    {
                        oStock.ProductId = detail.ProductId;
                        oStock.Quantity -= detail.Quantity;
                        oStock.StoreId = detail.StoreId;
                        db.SaveChanges();
                    }
                    else
                    {
                        Stock stock = new Stock();
                        stock.ProductId = detail.ProductId;
                        stock.Quantity = detail.Quantity;
                        stock.StoreId = detail.StoreId;
                        db.Add(stock);
                        db.SaveChanges();
                    }

                    var purchase = (from x in db.Sales
                                    where x.SaleId == saleVM.SaleId
                                    select x).FirstOrDefault();
                    if (purchase != null)
                    {
                        purchase.IsApprove = true;
                        db.SaveChanges();
                    }
                }

                #endregion
                return saleVM;
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                return saleVM;
            }
        }
    }
}
