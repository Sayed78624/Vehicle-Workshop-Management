using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleWorkShop.Data;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Repository
{
    public class RoleRP:IRole
    {
        private readonly WorkShopDbContext db;
        private readonly IMapper mapper;
        public RoleRP(WorkShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<List<RoleVM>> GetAll()
        {
            var list = await db.Roles.ToListAsync();
            var rolelist = mapper.Map<List<RoleVM>>(list);
            return rolelist;
        }
        public async Task<IActionResult> Create(RoleVM roleVM)
        {
            try
            {
                Role role = new Role
                {
                    RoleId = roleVM.RoleId,
                    RoleName = roleVM.RoleName,
                };
                db.Roles.Add(role);
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
            var roleid = db.Roles.Where(sid => sid.RoleId == id).FirstOrDefault();
            if (roleid != null)
            {
                db.Roles.Remove(roleid);
                await db.SaveChangesAsync();
                return new OkResult();
            }
            return new BadRequestResult();

        }
        public async Task<IActionResult> Update(RoleVM roleVM)
        {
            var roleid = await db.Roles.FirstOrDefaultAsync(a => a.RoleId == roleVM.RoleId);
            if (roleid == null)
            {
                return new NotFoundResult();
            }
            roleid.RoleId = roleVM.RoleId;
            roleid.RoleName = roleVM.RoleName;
            db.Roles.Update(roleid);
            await db.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<RoleVM> GetById(int id)
        {
            var roleId = await db.Roles.Where(x => x.RoleId == id).FirstOrDefaultAsync();
            var data = mapper.Map<RoleVM>(roleId);
            return data;
        }
    }
}
