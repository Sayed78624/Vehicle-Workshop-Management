using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleWorkShop.Data;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Repository
{
    public class TransferRP : ITransfer
    {
        private readonly WorkShopDbContext db;
        private readonly IMapper mapper;
        public TransferRP(WorkShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<TransferVM> Create(TransferVM transferVM)
        {
            try
            {
                var detailsdata = transferVM.Details.Select(d => new TransferDetail
                {
                    ProductId = d.ProductId,
                    Quantity = d.Quantity,
                    SourceStoreId= d.SourceStoreId,
                    DestinationStoreId= d.DestinationStoreId,
                }).ToList();

                Transfer transfer = new Transfer
                {
                    Description = transferVM.Description,
                    //IsApprove = transferVM.IsApprove,
                };
                db.Transfers.Add(transfer); 
               await db.SaveChangesAsync();
                foreach (var item in detailsdata)
                {
                    item.Tran_Id = transfer.Tran_Id;
                }
                db.TransferDetails.AddRange(detailsdata);   
               await db.SaveChangesAsync();
                return transferVM;
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine("Save Error: " + errorMessage);
                throw;

            }

        }

        public async Task<List<TransferVM>> GetAll()
        {
            var list =await db.Transfers
                .Include(t => t.TransferDetails)
                    .ThenInclude(td => td.Product)
                .Include(t => t.TransferDetails)
                    .ThenInclude(td => td.SourceStore)
                .Include(t => t.TransferDetails)
                    .ThenInclude(td => td.DestinationStore)
                .ToListAsync();
            var alldata = mapper.Map<List<TransferVM>>(list);
            return alldata;

        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var transfer = await db.Transfers
                    .Include(t => t.TransferDetails)
                    .FirstOrDefaultAsync(t => t.Tran_Id == id);

                if (transfer == null)
                {
                    return new OkResult();
                }

                db.TransferDetails.RemoveRange(transfer.TransferDetails);
                db.Transfers.Remove(transfer);
                await db.SaveChangesAsync();

                    return new OkResult();
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine("Delete Error: " + errorMessage);
                return new OkResult();
            }
        }

        public async Task<TransferVM> Approve(TransferVM modelVM)
        {
            try
            {
                #region Transfer
                foreach (var detail in modelVM.Details)
                {
                    #region Issue (from source-store)
                    Ledger ledger1 = new Ledger();
                    ledger1.Quantity = detail.Quantity;
                    ledger1.InventoryTypeId = 3; // transfer
                    ledger1.StockTypeId = 1; // issue
                    ledger1.StoreId = detail.SourceStoreId;
                    ledger1.ProductId = detail.ProductId;
                    //ledger1.UserId = Login UserID
                    db.Ledgers.Add(ledger1);
                    await db.SaveChangesAsync();

                    var oStock1 = (from x in db.Stocks
                                   where x.ProductId == detail.ProductId && x.StoreId == detail.SourceStoreId
                                   select x).FirstOrDefault();
                    if (oStock1 != null)
                    {
                        oStock1.ProductId = detail.ProductId;
                        oStock1.Quantity -= detail.Quantity;
                        oStock1.StoreId = detail.SourceStoreId;
                        db.SaveChanges();
                    }
                    else
                    {
                        Stock stock1 = new Stock();
                        stock1.ProductId = detail.ProductId;
                        stock1.Quantity = detail.Quantity;
                        stock1.StoreId = detail.SourceStoreId;
                        db.Add(stock1);
                        db.SaveChanges();
                    }
                    #endregion
                    #region Receive (from source-store)
                    Ledger ledger2 = new Ledger();
                    ledger2.Quantity = detail.Quantity;
                    ledger2.InventoryTypeId = 3; // transfer
                    ledger2.StockTypeId = 2; // receive
                    ledger2.StoreId = detail.SourceStoreId;
                    ledger2.ProductId = detail.ProductId;
                    //ledger2.UserId = Login UserID
                    db.Ledgers.Add(ledger2);
                    await db.SaveChangesAsync();

                    var oStock2 = (from x in db.Stocks
                                   where x.ProductId == detail.ProductId && x.StoreId == detail.DestinationStoreId
                                   select x).FirstOrDefault();
                    if (oStock2 != null)
                    {
                        oStock2.ProductId = detail.ProductId;
                        oStock2.Quantity += detail.Quantity;
                        oStock2.StoreId = detail.SourceStoreId;
                        db.SaveChanges();
                    }
                    else
                    {
                        Stock stock2 = new Stock();
                        stock2.ProductId = detail.ProductId;
                        stock2.Quantity = detail.Quantity;
                        stock2.StoreId = detail.SourceStoreId;
                        db.Add(stock2);
                        db.SaveChanges();
                    }
                    #endregion
                    #region Approve
                    var model = (from x in db.Transfers
                                 where x.Tran_Id == modelVM.Tran_Id
                                 select x).FirstOrDefault();
                    if (model != null)
                    {
                        model.IsApprove = true;
                        db.SaveChanges();
                    }
                    #endregion
                }
                #endregion
                return modelVM;
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                return modelVM;
            }
        }

        public async Task<TransferVM> GetId(int id)
        {
            TransferVM oTransfer = null;

            var transfer = await (from t in db.Transfers
                                  where t.Tran_Id == id
                                  select new TransferVM
                                  {
                                      Tran_Id = t.Tran_Id,
                                      Description = t.Description,
                                      IsApprove = t.IsApprove
                                  }).FirstOrDefaultAsync();

            if (transfer != null)
            {
                var transferDetails = await (from d in db.TransferDetails
                                             join p in db.Products on d.ProductId equals p.ProductId
                                             join s1 in db.Stores on d.SourceStoreId equals s1.StoreId
                                             join s2 in db.Stores on d.DestinationStoreId equals s2.StoreId
                                             where d.Tran_Id == id
                                             select new TransferDetailVM
                                             {
                                                 ProductId = d.ProductId,
                                                 ProductName = p.ProductName,
                                                 Quantity = d.Quantity,
                                                 SourceStoreId = d.SourceStoreId,
                                                 DestinationStoreId = d.DestinationStoreId,
                                                 SourceStoreName = s1.Name,
                                                 DestinationStoreName = s2.Name
                                             }).ToListAsync();

                oTransfer = new TransferVM
                {
                    Tran_Id = transfer.Tran_Id,
                    Description = transfer.Description,
                    IsApprove = transfer.IsApprove,
                    Details = transferDetails
                };
            }

            return oTransfer;
        }

        public async Task<TransferInvoiceVM> GetInvoice(int id)
        {
            var data =await db.Transfers.Include(d => d.TransferDetails).FirstOrDefaultAsync(i => i.Tran_Id == id);
            if (data == null) return null;
            var invoice = new TransferInvoiceVM
            {
                InvoiceId = data.Tran_Id,
                Description = data.Description,
                Details = await (from d in db.TransferDetails
                                 join p in db.Products on d.ProductId equals p.ProductId
                                 join s1 in db.Stores on d.SourceStoreId equals s1.StoreId
                                 join s2 in db.Stores on d.DestinationStoreId equals s2.StoreId
                                 where d.Tran_Id == id
                                 select new TransferDetailVM
                                 {
                                     ProductId = d.ProductId,
                                     ProductName = p.ProductName,
                                     Quantity = d.Quantity,
                                     SourceStoreName = s1.Name,
                                     DestinationStoreName = s2.Name
                                 }).ToListAsync(),
            };
            return invoice;
        }
    }
}
