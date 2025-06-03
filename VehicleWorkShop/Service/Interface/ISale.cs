using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Interface
{
    public interface ISale
    {
        Task<IActionResult> Create(SaleVM saleVM);
        Task<List<SaleVM>> GetAll();
        Task<IActionResult> Delete(int id);

    }
}
