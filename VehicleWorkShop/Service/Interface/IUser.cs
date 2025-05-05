using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Interface
{
    public interface IUser
    {
        Task<IActionResult> Create(UserVM userVM);
        Task<List<UserVM>> GetAll();
        Task<IActionResult> Delete(int id);
        Task<IActionResult> Update(UserVM userVM);
        Task<UserVM> GetById(int id);

    }
}
