using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Interface
{
    public interface IPurchaseDetail
    {
        Task<IActionResult> Create(PurchaseDetailVM purchaseDetailVM);
        Task<List<PurchaseDetailVM>> GetAll();
    }
}
