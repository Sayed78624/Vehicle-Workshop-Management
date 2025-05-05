using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleWorkShop.Data;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Repository
{
    public class CategoryRP : ICategory
    {
        private readonly WorkShopDbContext db;
        private readonly IMapper mapper;
        public CategoryRP(WorkShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<List<CategoryVM>> GetAll()
        {
            var list = await db.Categories.ToListAsync();
            var categorylist = mapper.Map<List<CategoryVM>>(list);
            return categorylist;
        }
        public async Task<IActionResult> Create(CategoryVM categoryVM)
        {
            try
            {
                Category category = new Category
                {
                    CategoryId = categoryVM.CategoryId,
                    Name = categoryVM.Name,
                    IsActive = categoryVM.IsActive,
                };
                db.Categories.Add(category);
                await db.SaveChangesAsync();
                return new JsonResult(new { success = true, message = "Workshop created successfully!" });
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                return new JsonResult(ErrorMessage);
            }
        }
        public async Task<IList<Category>> GetAllCategories()
        {
            return await db.Categories.ToListAsync();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var cateid = db.Categories.Where(sid => sid.CategoryId == id).FirstOrDefault();
            if (cateid != null)
            {
                db.Categories.Remove(cateid);
                await db.SaveChangesAsync();
                return new OkResult();
            }
            return new BadRequestResult();

        }

        public async Task<IActionResult> Update(CategoryVM categoryVM)
        {
            var catlisrt = await db.Categories.FirstOrDefaultAsync(a => a.CategoryId == categoryVM.CategoryId);
            if (catlisrt == null)
            {
                    return new NotFoundResult();
            }
            catlisrt.CategoryId = categoryVM.CategoryId;
            catlisrt.Name = categoryVM.Name;
            catlisrt.IsActive = categoryVM.IsActive;
            db.Categories.Update(catlisrt);
            await db.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<CategoryVM> GetById(int id)
        {
            var cId = await db.Categories.Where(x => x.CategoryId == id).FirstOrDefaultAsync();
            var data = mapper.Map<CategoryVM>(cId);
            return data;
        }
    }
}
