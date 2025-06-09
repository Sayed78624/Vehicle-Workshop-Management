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

        public async Task<TransferVM> Approve(TransferVM transferVM)
        {
            try
            {
                foreach (var detail in transferVM.Details)
                {
                    //  Source Store Stock 
                    var sourceStock = await db.Stocks
                        .FirstOrDefaultAsync(s => s.ProductId == detail.ProductId && s.StoreId == detail.SourceStoreId);

                    if (sourceStock != null && sourceStock.Quantity >= detail.Quantity)
                    {
                        sourceStock.Quantity -= detail.Quantity;
                        db.Stocks.Update(sourceStock);
                    }
                    else
                    {
                        
                        throw new Exception("Insufficient stock in source store.");
                    }

                    //  Destination Store Stock ব
                    var destStock = await db.Stocks
                        .FirstOrDefaultAsync(s => s.ProductId == detail.ProductId && s.StoreId == detail.DestinationStoreId);

                    if (destStock != null)
                    {
                        destStock.Quantity += detail.Quantity;
                        db.Stocks.Update(destStock);
                    }
                    else
                    {
                        var newStock = new Stock
                        {
                            ProductId = detail.ProductId,
                            StoreId = detail.DestinationStoreId,
                            Quantity = detail.Quantity
                        };
                        await db.Stocks.AddAsync(newStock);
                    }

                    //  Ledger Entry 
                    var ledger = new Ledger
                    {
                        ProductId = detail.ProductId,
                        Quantity = detail.Quantity,
                        InventoryTypeId = 3, // Transfer
                        StockTypeId = 3, // Movement
                        StoreId = detail.DestinationStoreId,
                       
                        Price = 0 // Optional if applicable
                    };
                    await db.Ledgers.AddAsync(ledger);
                }

                // Update IsApprove
                var transfer = await db.Transfers.FirstOrDefaultAsync(t => t.Tran_Id == transferVM.Tran_Id);
                if (transfer != null)
                {
                    transfer.IsApprove = true;
                    db.Transfers.Update(transfer);
                }

                await db.SaveChangesAsync();

                return transferVM;
            }
            catch (Exception ex)
            {
                var error = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine("Approve Error: " + error);
                throw;
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


    }
}
