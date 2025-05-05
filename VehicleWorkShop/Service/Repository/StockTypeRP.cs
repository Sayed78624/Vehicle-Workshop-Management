using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleWorkShop.Data;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Repository
{
    public class StockTypeRP: IStockType
    {
        private readonly WorkShopDbContext db;
        private readonly IMapper mapper;
        public StockTypeRP(WorkShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<List<StockTypeVM>> GetAll()
        {
            var list = await db.StockTypes.ToArrayAsync();
            var Typlist = mapper.Map<List<StockTypeVM>>(list);
            return Typlist;
        }
        public async Task<IActionResult> Create(StockTypeVM stockTypeVM)
        {
            try
            {
                StockType stockType = new StockType
                {
                    StockTypeName = stockTypeVM.StockTypeName,
                };
                db.StockTypes.Add(stockType);
                await db.SaveChangesAsync();
                return new JsonResult(new { success = true, message = "Workshop created successfully!" });
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                return new JsonResult(ErrorMessage);
            }
        }

        public async Task<IList<StockType>> GetAllStockType()
        {
           return await db.StockTypes.ToListAsync();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var supplierid = db.StockTypes.Where(sid => sid.StockTypeId == id).FirstOrDefault();
            if (supplierid != null)
            {
                db.StockTypes.Remove(supplierid);
                await db.SaveChangesAsync();
                return new OkResult();
            }
            return new BadRequestResult();

        }
        public async Task<IActionResult> Update(StockTypeVM supplierVM)
        {
            var supplieredit = await db.StockTypes.FirstOrDefaultAsync(a => a.StockTypeId == supplierVM.StockTypeId);
            if (supplieredit == null)
            {
                    return new NotFoundResult();
            }
            supplieredit.StockTypeId = supplierVM.StockTypeId;
            supplieredit.StockTypeName = supplierVM.StockTypeName;
            db.StockTypes.Update(supplieredit);
            await db.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<StockTypeVM> GetById(int id)
        {
            var supplierId = await db.StockTypes.Where(x => x.StockTypeId == id).FirstOrDefaultAsync();
            var data = mapper.Map<StockTypeVM>(supplierId);
            return data;
        }
    }
}
