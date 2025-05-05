using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Interface
{
    public interface IPurchase
    {
        Task<IActionResult> Create(Purchase purchase);
        //Task<IActionResult> ApprovePurchase(int purchaseId);
        //Task<PurchaseVM> GetPurchaseById(int id);
        Task<List<Purchase>> GetAll();

    }
}
