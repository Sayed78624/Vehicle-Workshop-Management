using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Interface
{
    public interface IStore
    {
        Task<IActionResult> Create(StoreVM storeVM);
        Task<List<StoreVM>> GetAll();
        Task<IList<Store>> GetAllStores();
    }
}
