using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
using VehicleWorkShop.Utilities;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Interface
{
    public interface ISupplier
    {
        Task<IActionResult> Create(SupplierVM supplierVM);
        Task<List<SupplierVM>> GetAll();
        Task<IActionResult> Delete(int id);
        Task<IList<Supplier>> GetAllSupplier();
        Task<IActionResult> Update(SupplierVM supplierVM);
        Task<SupplierVM> GetById(int id);

    }
}
