using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VehicleWorkShop.Data;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.Utilities;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Repository
{
    public class WorkShopRP : IWorkShop
    {
        private readonly WorkShopDbContext db;
        private readonly IMapper mapper;
        public WorkShopRP(WorkShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Create(WorkShopVM workshopvm)
        {
            try
            {
                var workshp = new WorkShop
                {
                    WorkShopId = workshopvm.WorkShopId,
                    WorkShopName = workshopvm.WorkShopName,
                    NumberOfBay = workshopvm.NumberOfBay,
                    NumberOfLevel = workshopvm.NumberOfLevel,
                };
                db.WorkShops.Add(workshp);
                await db.SaveChangesAsync();
                return new JsonResult(new { success = true, message = "Workshop created successfully!" });
            }
            catch (Exception ex)
            {
                var ErrorMessage= ex.Message;
                 return new JsonResult(ErrorMessage);
            }
        }
        public async Task<List<WorkShopVM>> WorkShopList()
        {
            var list = await db.WorkShops.ToListAsync();
            var workshoplist = mapper.Map<List<WorkShopVM>>(list);
            return workshoplist;
        }
        public async Task<IActionResult> Delete(int id)
        {
            var supplierid = db.WorkShops.Where(sid => sid.WorkShopId == id).FirstOrDefault();
            if (supplierid != null)
            {
                db.WorkShops.Remove(supplierid);
                await db.SaveChangesAsync();
                return new OkResult();
            }
            return new BadRequestResult();

        }
        public async Task<IActionResult> Update(WorkShopVM supplierVM)
        {
            var supplieredit = await db.WorkShops.FirstOrDefaultAsync(a => a.WorkShopId == supplierVM.WorkShopId);
            if (supplieredit == null)
            {
                return new NotFoundResult();
            }
            supplieredit.WorkShopId = supplierVM.WorkShopId;
            supplieredit.WorkShopName = supplierVM.WorkShopName;
            supplieredit.NumberOfBay = supplierVM.NumberOfBay;
            supplieredit.NumberOfLevel = supplierVM.NumberOfLevel;
            db.WorkShops.Update(supplieredit);
            await db.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<WorkShopVM> GetById(int id)
        {
            var supplierId = await db.WorkShops.Where(x => x.WorkShopId == id).FirstOrDefaultAsync();
            var data = mapper.Map<WorkShopVM>(supplierId);
            return data;
        }

        public async Task<IList<WorkShop>> GetAllWorkshops()
        {
            var list = await db.WorkShops.ToListAsync();
            return list;
        }
    }
}
