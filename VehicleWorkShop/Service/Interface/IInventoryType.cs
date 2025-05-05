using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Interface
{
    public interface IInventoryType
    {
        Task<IActionResult> Create(InventoryTypeVM inventoryTypeVM);
        Task<List<InventoryTypeVM>> GetAll();
        Task<IList<InventoryType>> GetAllInventoryType();
        Task<IActionResult> Delete(int id);
        Task<IActionResult> Update(InventoryTypeVM inventoryTypeVM);
        Task<InventoryTypeVM> GetById(int id);
    }
}
