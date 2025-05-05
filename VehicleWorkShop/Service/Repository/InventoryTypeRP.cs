using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleWorkShop.Data;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Repository
{
    public class InventoryTypeRP : IInventoryType
    {
        private readonly WorkShopDbContext db;
        private readonly IMapper mapper;
        public InventoryTypeRP(WorkShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<List<InventoryTypeVM>> GetAll()
        {
            var list = await db.InventoryTypes.ToArrayAsync();
            var datalist = mapper.Map<List<InventoryTypeVM>>(list);
            return datalist;
        }
        public async Task<IActionResult> Create(InventoryTypeVM inventoryTypeVM)
        {
            try
            {
                InventoryType inventoryType = new InventoryType
                {
                    InventoryTypeId = inventoryTypeVM.InventoryTypeId,
                    Name = inventoryTypeVM.Name,
                    Remarks = inventoryTypeVM.Remarks
                };
                db.InventoryTypes.Add(inventoryType);   
                await db.SaveChangesAsync();
                return new JsonResult(new {success=true, message="InventoryType Created Successfully"});
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                return new JsonResult(ErrorMessage);
            }
        }

        public async Task<IList<InventoryType>> GetAllInventoryType()
        {
          return await  db.InventoryTypes.ToListAsync();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var roleid = db.InventoryTypes.Where(sid => sid.InventoryTypeId == id).FirstOrDefault();
            if (roleid != null)
            {
                db.InventoryTypes.Remove(roleid);
                await db.SaveChangesAsync();
                return new OkResult();
            }
            return new BadRequestResult();

        }
        public async Task<IActionResult> Update(InventoryTypeVM roleVM)
        {
            var roleid = await db.InventoryTypes.FirstOrDefaultAsync(a => a.InventoryTypeId == roleVM.InventoryTypeId);
            if (roleid == null)
            {
                return new NotFoundResult();
            }
            roleid.InventoryTypeId = roleVM.InventoryTypeId;
            roleid.Name = roleVM.Name;
            roleid.Remarks = roleVM.Remarks;
            db.InventoryTypes.Update(roleid);
            await db.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<InventoryTypeVM> GetById(int id)
        {
            var roleId = await db.InventoryTypes.Where(x => x.InventoryTypeId == id).FirstOrDefaultAsync();
            var data = mapper.Map<InventoryTypeVM>(roleId);
            return data;
        }
    }
}
