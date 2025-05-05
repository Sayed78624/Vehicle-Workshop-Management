using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Interface
{
    public interface IProduct
    {
        Task<IActionResult> Create(ProductVM productVM);
        Task<List<ProductVM>> GetAll();
        Task<IList<Product>> GetAllProducts();
    }
}
