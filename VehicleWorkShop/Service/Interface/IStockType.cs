using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Interface
{
    public interface IStockType
    {
        Task<IActionResult> Create(StockTypeVM stockTypeVM);
        Task<List<StockTypeVM>> GetAll();
        Task<IList<StockType>> GetAllStockType();
        Task<IActionResult> Delete(int id);
        Task<IActionResult> Update(StockTypeVM stockTypeVM);
        Task<StockTypeVM> GetById(int id);
    }
}
