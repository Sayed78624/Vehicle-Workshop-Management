using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleWorkShop.Data;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Repository
{
    public class VehicleModelRP : IVehicleModel
    {
        private readonly WorkShopDbContext db;
        private readonly IMapper mapper;
        public VehicleModelRP(WorkShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<List<VehicleModelVM>> GetAll()
        {
            var list = await db.VehicleModels.ToListAsync();
            var rolelist = mapper.Map<List<VehicleModelVM>>(list);
            return rolelist;
        }
        public async Task<IActionResult> Create(VehicleModelVM vehicleModelVM)
        {
            try
            {
                VehicleModel model = new VehicleModel
                {
                    ModelId = vehicleModelVM.ModelId,
                    ModelName = vehicleModelVM.ModelName,
                };
                db.VehicleModels.Add(model);
                await db.SaveChangesAsync();
                return new JsonResult(new { success = true, message = "Workshop created successfully!" });
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                return new JsonResult(ErrorMessage);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var modelid = db.VehicleModels.Where(sid => sid.ModelId == id).FirstOrDefault();
            if (modelid != null)
            {
                db.VehicleModels.Remove(modelid);
                await db.SaveChangesAsync();
                return new OkResult();
            }
            return new BadRequestResult();

        }
        public async Task<IActionResult> Update(VehicleModelVM vehicleModelVM)
        {
            var modelid = await db.VehicleModels.FirstOrDefaultAsync(a => a.ModelId == vehicleModelVM.ModelId);
            if (modelid == null)
            {
                return new NotFoundResult();
            }
            modelid.ModelId = vehicleModelVM.ModelId;
            modelid.ModelName = vehicleModelVM.ModelName;
            db.VehicleModels.Update(modelid);
            await db.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<VehicleModelVM> GetById(int id)
        {
            var modelId = await db.VehicleModels.Where(x => x.ModelId == id).FirstOrDefaultAsync();
            var data = mapper.Map<VehicleModelVM>(modelId);
            return data;
        }

        public async Task<IList<VehicleModel>> GetAllModels()
        {
            return await db.VehicleModels.ToListAsync();
        }
    }
}
