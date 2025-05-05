using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Interface
{
    public interface ICategory
    {
        Task<IActionResult> Create(CategoryVM categoryVM);
        Task<List<CategoryVM>> GetAll();
        Task<IList<Category>> GetAllCategories();
        Task<IActionResult> Delete(int id);
        Task<IActionResult> Update(CategoryVM categoryVM);
        Task<CategoryVM> GetById(int id);
    }
}
