using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Interface
{
    public interface IProduct
    {
        Task<IActionResult> Create(ProductVM productVM);
        Task<List<ProductVM>> GetAll(string? searchTerm = null);
        Task<IList<Product>> GetAllProducts();
        Task<IActionResult> Delete(int id);
        Task<IActionResult> Update(ProductVM productVM);
        Task<ProductVM> GetById(int id);
    }
}
