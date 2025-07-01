using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Interface
{
    public interface IPaymentType
    {
        Task<IActionResult> Create(PaymentTypeVM paymentTypeVM);
        Task<List<PaymentTypeVM>> GetAll();
        Task<IActionResult> Delete(int id);
        Task<IActionResult> Update(PaymentTypeVM paymentTypeVM);
        Task<PaymentTypeVM> GetId(int id);
        Task<IList<PaymentType>> GetAllType();
    }
}
