using AutoMapper;
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

        public async Task<List<PurchaseVM>> GetAll()
        {
            var list = await (from p in db.Purchases
                              join s in db.Suppliers on p.SupplierId equals s.SupplierId
                              select new PurchaseVM()
                              {
                                  SupplierId = p.SupplierId,
                                  Description = p.Description,
                                  GrandTotal = p.GrandTotal,
                                  SupplierName = s.SupplierName,
                                  PurchaseId = p.PurchaseId,
                                  IsApprove = p.IsApprove,
                              }).ToListAsync();
            return list;
        }

        public async Task<PurchaseVM> GetById(int id)
        {
            PurchaseVM oPurchase = null; decimal grandTotal = 0;
            var purchase = await (from p in db.Purchases
                                  join s in db.Suppliers on p.SupplierId equals s.SupplierId
                                  where p.PurchaseId == id
                                  select new PurchaseVM()
                                  {
                                      SupplierId = p.SupplierId,
                                      Description = p.Description,
                                      GrandTotal = p.GrandTotal,
                                      SupplierName = s.SupplierName,
                                      PurchaseId = p.PurchaseId,
                                      IsApprove = p.IsApprove,
                                  }).FirstOrDefaultAsync();
            if (purchase != null)
            {
                var purchasesDetailsList = await (from pd in db.PurchasesDetails
                                                  join p in db.Products on pd.ProductId equals p.ProductId
                                                  join s in db.Stores on pd.StoreId equals s.StoreId
                                                  join m in db.VehicleModels on pd.ModelId equals m.ModelId
                                                  where pd.PurchaseId == id
                                                  select new PurchaseDetailVM()
                                                  {
                                                      PurchaseDetailId = pd.PurchaseDetailId,
                                                      Quantity = pd.Quantity,
                                                      Price = pd.Price,
                                                      PurchaseId = pd.PurchaseId,
                                                      ProductId = pd.ProductId,
                                                      StoreId = pd.StoreId,
                                                      SubTotal = pd.SubTotal,
                                                      Vat = pd.Vat,
                                                      ProductName = p.ProductName,
                                                      StoreName = s.Name,
                                                      ModelId = pd.ModelId,
                                                      ModelName = m.ModelName,
                                                  }).ToListAsync();
                foreach (var item in purchasesDetailsList)
                {
                    grandTotal += item.SubTotal;
                }
                oPurchase = new PurchaseVM()
                {
                    PurchaseId = purchase.PurchaseId,
                    Description = purchase.Description,
                    GrandTotal = grandTotal,
                    IsApprove = purchase.IsApprove,
                    SupplierId = purchase.SupplierId,
                    PurchaseDetails = purchasesDetailsList
                };
            }
            return oPurchase;
        }

        public async Task<PurchaseVM> CreateMaster(PurchaseVM purchaseVM)
        {
            try
            {
                #region Master insert/update
                var purchase = await db.Purchases.Where(x => x.PurchaseId == purchaseVM.PurchaseId).FirstOrDefaultAsync();
                if (purchase == null)
                {
                    purchase = new Purchase
                    {
                        Description = purchaseVM.Description,
                        SupplierId = purchaseVM.SupplierId,
                        GrandTotal = purchaseVM.GrandTotal,
                        IsApprove = purchaseVM.IsApprove,
                    };
                    db.Purchases.Add(purchase);
                    await db.SaveChangesAsync();
                    purchaseVM.PurchaseId = purchase.PurchaseId;
                }
                else
                {
                    purchase.Description = purchaseVM.Description;
                    purchase.SupplierId = purchaseVM.SupplierId;
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

        public async Task<PurchaseDetailVM> CreateDetail(PurchaseDetailVM detail)
        {
            try
            {
                var purchaseDetail = new PurchaseDetail
                {
                    PurchaseId = detail.PurchaseId,
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity,
                    Price = detail.Price,
                    Vat = detail.Vat,
                    SubTotal = (detail.Quantity * detail.Price) + detail.Vat,
                    StoreId = detail.StoreId,
                    ModelId = detail.ModelId,
                };
                db.PurchasesDetails.Add(purchaseDetail);
                await db.SaveChangesAsync();
                detail.PurchaseId = purchaseDetail.PurchaseId;
                detail.PurchaseDetailId = purchaseDetail.PurchaseDetailId;
                return detail;
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                return detail;
            }
        }

        public async Task<PurchaseVM> Approve(PurchaseVM purchaseVM)
        {
            try
            {
                #region Insert Ledger + Update Stock
                foreach (var detail in purchaseVM.PurchaseDetails)
                {
                    Ledger ledger = new Ledger();
                    ledger.Price = detail.Price;
                    ledger.Quantity = detail.Quantity;
                    ledger.Price = detail.Price;
                    ledger.InventoryTypeId = 1; // purchase
                    ledger.StockTypeId = 2; // receive
                    ledger.StoreId = detail.StoreId;
                    ledger.ProductId = detail.ProductId;
                    //ledger.UserId = Login UserID
                    db.Ledgers.Add(ledger);
                    await db.SaveChangesAsync();

                    var oStock = (from x in db.Stocks
                                  where x.ProductId == detail.ProductId && x.StoreId == detail.StoreId
                                  select x).FirstOrDefault();
                    if (oStock != null)
                    {
                        oStock.ProductId = detail.ProductId;
                        oStock.Quantity += detail.Quantity;
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

                    var purchase = (from x in db.Purchases
                                    where x.PurchaseId == purchaseVM.PurchaseId
                                    select x).FirstOrDefault();
                    if (purchase != null)
                    {
                        purchase.IsApprove = true;
                        db.SaveChanges();
                    }
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

        public async Task<PurchaseDetailVM> RemoveDetail(int detailID)
        {
            PurchaseDetailVM purchaseDetailVM = new PurchaseDetailVM();
            try
            {
                var purchaseDetail = await db.PurchasesDetails.Where(x => x.PurchaseId == detailID).FirstOrDefaultAsync();
                if (purchaseDetail != null)
                {
                    db.PurchasesDetails.Remove(purchaseDetail);
                    await db.SaveChangesAsync();
                    purchaseDetailVM.PurchaseDetailId = purchaseDetail.PurchaseDetailId;
                    purchaseDetailVM.PurchaseId = purchaseDetail.PurchaseId;
                }
                return purchaseDetailVM;
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                //return new BadRequestResult();
                return purchaseDetailVM;
            }
        }

        public async Task<PurchaseInvoice> GetInvoice(int id)
        {
            var data = await db.Purchases.Include(s => s.Supplier).Include(
                p => p.PurchaseDetails).ThenInclude(p => p.Product).ThenInclude(
                v => v.VehicleModel).FirstOrDefaultAsync(
                i => i.PurchaseId == id);
            if (data == null) return null;

            var pinvoice = new PurchaseInvoice
            {
                InvoiceId = data.PurchaseId,
                PurchaseDate = DateTime.Now,
                SupplierName = data.Supplier.SupplierName,
                ManagerName = data.Supplier.Manager,
                Mobile = data.Supplier.Mobile,
                Address = data.Supplier.Address,
                GrandTotal = data.PurchaseDetails.Sum(s => s.SubTotal),
                Details = data.PurchaseDetails.Select(a => new PurchaseDetailVM
                {
                    PurchaseDetailId = a.PurchaseDetailId,
                    ProductName = a.Product.ProductName,
                    Quantity = a.Quantity,
                    ModelName = a.VehicleModel.ModelName,
                    Price = a.Price,
                    Vat = a.Vat,
                    SubTotal = a.SubTotal,

                }).ToList(),
            };
            return pinvoice;
        }
    }
}



