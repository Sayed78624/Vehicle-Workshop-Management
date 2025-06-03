using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Interface
{
    public interface IVehicleModel
    {
        Task<IActionResult> Create(VehicleModelVM vehicleModelVM);
        Task<List<VehicleModelVM>> GetAll();
        Task<IActionResult> Delete(int id);
        Task<IActionResult> Update(VehicleModelVM vehicleModelVM);
        Task<VehicleModelVM> GetById(int id);
        Task<IList<VehicleModel>> GetAllModels();
    }
}
