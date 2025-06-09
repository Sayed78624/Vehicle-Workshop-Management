using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Interface
{
    public interface ITransfer
    {
        Task<TransferVM> Create(TransferVM transferVM);
        Task<List<TransferVM>> GetAll();
        Task<IActionResult> Delete(int id);
        Task<TransferVM> Approve(TransferVM transferVM);
        Task<TransferVM> GetId(int id);
    }
}
