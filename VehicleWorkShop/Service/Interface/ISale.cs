using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Interface
{
    public interface ISale
    {
        Task<List<SaleVM>> GetAll();
        Task<SaleVM> GetById(int id);
        Task<SaleVM> CreateMaster(SaleVM saleVM);
        Task<SaleDetailVM> CreateDetail(SaleDetailVM detail);
        Task<SaleVM> Approve(SaleVM saleVM);

    }
}
