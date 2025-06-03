using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.Utilities;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Interface
{
    public interface IWorkShop
    {
        Task<IActionResult> Create(WorkShopVM workshop);
        Task<List<WorkShopVM>> WorkShopList();
        Task<IActionResult> Delete(int id);
        Task<IActionResult> Update(WorkShopVM workShopVM);
        Task<WorkShopVM> GetById(int id);
        Task<IList<WorkShop>> GetAllWorkshops();
    }
}
