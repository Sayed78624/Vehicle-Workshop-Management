using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Interface
{
    public interface ICustomer
    {
        Task<IActionResult> Create(CustomerVM customerVM);
        Task<List<CustomerVM>> GetAll();
        Task<IList<Customer>> GetAllCustomers();
        Task<IActionResult> Delete(int id);
    }
}
