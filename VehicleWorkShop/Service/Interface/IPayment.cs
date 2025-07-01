using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Interface
{
    public interface IPayment
    {
        Task<IActionResult> CreatePayment(PaymentVM paymentVM);
    }
}
