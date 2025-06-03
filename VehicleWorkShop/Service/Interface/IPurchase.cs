using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Interface
{
    public interface IPurchase
    {

        Task<List<PurchaseVM>> GetAll();
        Task<PurchaseVM> GetById(int id);
        Task<PurchaseVM> CreateMaster(PurchaseVM purchaseVM);
        Task<PurchaseDetailVM> CreateDetail(PurchaseDetailVM detail);
        Task<PurchaseVM> Approve(PurchaseVM purchaseVM);
        Task<PurchaseDetailVM> RemoveDetail(int id);
    }
}
