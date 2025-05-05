using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleWorkShop.Data;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Repository
{
    public class StoreRP: IStore
    {
        private readonly WorkShopDbContext db;
        private readonly IMapper mapper;
        public StoreRP(WorkShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<List<StoreVM>> GetAll()
        {
            var list = await db.Stores.ToListAsync();
            var storelist = mapper.Map<List<StoreVM>>(list);
            return storelist;
        }
        public async Task<IActionResult> Create(StoreVM storeVM)
        {
            try
            {
                Store store = new Store
                {
                    StoreId = storeVM.StoreId,
                    Name = storeVM.Name,
                };
                db.Stores.Add(store);
                await db.SaveChangesAsync();
                return new JsonResult(new { success = true, message = "Workshop created successfully!" });
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                return new JsonResult(ErrorMessage);
            }
        }

        public async Task<IList<Store>> GetAllStores()
        {
           return await db.Stores.ToListAsync();
        }
    }
    
    }

