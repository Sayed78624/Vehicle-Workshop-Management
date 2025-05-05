using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleWorkShop.Data;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Repository
{
    public class UserRP:IUser
    {
        private readonly WorkShopDbContext db;
        private readonly IMapper mapper;
        public UserRP(WorkShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<List<UserVM>> GetAll()
        {
            var list = await db.Users.ToListAsync();
            var userlist = mapper.Map<List<UserVM>>(list);
            return userlist;
        }
        public async Task<IActionResult> Create(UserVM userVM)
        {
            try
            {
                Users users = new Users
                {
                    UserId = userVM.UserId,
                    UserName = userVM.UserName,
                    Mobile = userVM.Mobile,
                    Address = userVM.Address,
                    Email = userVM.Email,
                    IsActive = userVM.IsActive
                };
                db.Users.Add(users);
                await db.SaveChangesAsync();
                return new JsonResult(new { success = true, message = "Supplier created successfully!" });
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                return new JsonResult(ErrorMessage);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var userid = db.Users.Where(sid => sid.UserId == id).FirstOrDefault();
            if (userid != null)
            {
                db.Users.Remove(userid);
                await db.SaveChangesAsync();
                return new OkResult();
            }
            return new BadRequestResult();

        }
        public async Task<IActionResult> Update(UserVM userVM)
        {
            var useredit = await db.Users.FirstOrDefaultAsync(a => a.UserId == userVM.UserId);
            if (useredit == null)
            {
                if (useredit == null)
                    return new NotFoundResult();
            }
            useredit.UserId = userVM.UserId;
            useredit.UserName = userVM.UserName;
            useredit.Mobile = userVM.Mobile;
            useredit.Address = userVM.Address;
            useredit.IsActive = userVM.IsActive;
            db.Users.Update(useredit);
            await db.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<UserVM> GetById(int id)
        {
            var userId = await db.Users.Where(x => x.UserId == id).FirstOrDefaultAsync();
            var data = mapper.Map<UserVM>(userId);
            return data;
        }
    }
}
