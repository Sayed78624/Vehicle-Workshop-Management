using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleWorkShop.Data;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Repository
{
    public class SupplierRP : ISupplier
    {
        private readonly WorkShopDbContext db;
        private readonly IMapper mapper;
        public SupplierRP(WorkShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<List<SupplierVM>> GetAll()
        {
            var list = await db.Suppliers.ToListAsync();
            var supplierlist = mapper.Map<List<SupplierVM>>(list);
            return supplierlist;
        } 
        public async Task<IActionResult> Create(SupplierVM supplierVM)
        {
            try
            {
                Supplier supplier = new Supplier
                {
                    SupplierId = supplierVM.SupplierId,
                    SupplierName = supplierVM.SupplierName,
                    Mobile = supplierVM.Mobile,
                    Address = supplierVM.Address,
                    Manager = supplierVM.Manager
                };
                db.Suppliers.Add(supplier);
                await db.SaveChangesAsync();
                return new JsonResult(new { success = true, message = "Supplier created successfully!" });
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                return new JsonResult(ErrorMessage);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var supplierid = db.Suppliers.Where(sid => sid.SupplierId == id).FirstOrDefault();
            if (supplierid != null)
            {
                db.Suppliers.Remove(supplierid);
                await db.SaveChangesAsync();
                return new OkResult();
            }
            return new BadRequestResult();

        }

        public async Task<IList<Supplier>> GetAllSupplier()
        {
            return await db.Suppliers.ToListAsync();
        }

        public async Task<IActionResult> Update(SupplierVM supplierVM)
        {
            var supplieredit =await db.Suppliers.FirstOrDefaultAsync(a =>a.SupplierId == supplierVM.SupplierId);
            if (supplieredit == null)
            {
                if (supplieredit == null)
                    return new NotFoundResult();
            }
            supplieredit.SupplierId = supplierVM.SupplierId;
            supplieredit.SupplierName = supplierVM.SupplierName;
            supplieredit.Mobile = supplierVM.Mobile;
            supplieredit.Address = supplierVM.Address;
            supplieredit.Manager = supplierVM.Manager;
            db.Suppliers.Update(supplieredit);
            await db.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<SupplierVM> GetById(int id)
        {
            var supplierId = await db.Suppliers.Where(x => x.SupplierId == id).FirstOrDefaultAsync();
            var data = mapper.Map<SupplierVM>(supplierId);
            return data;
        }
    }
}

