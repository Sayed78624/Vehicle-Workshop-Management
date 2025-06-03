using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleWorkShop.Data;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Repository
{
    public class CustomerRP : ICustomer
    {
        private readonly WorkShopDbContext db;
        private readonly IMapper mapper;
        public CustomerRP(WorkShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Create(CustomerVM customerVM)
        {
            try
            {
                Customer customer = new Customer()
                {
                    CustomerId = customerVM.CustomerId,
                    Name = customerVM.Name,
                    Mobile = customerVM.Mobile,
                    Email = customerVM.Email,
                    Address = customerVM.Address
                };
                db.Customers.Add(customer);
                await db.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return new JsonResult(message);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customer = db.Customers.Where(sid => sid.CustomerId == id).FirstOrDefault();
            if (customer != null)
            {
                db.Customers.Remove(customer);
               await db.SaveChangesAsync();
                return new OkResult();
            }
            return new BadRequestResult();
        }

        public async Task<List<CustomerVM>> GetAll()
        {
           var list = await db.Customers.ToListAsync();
            var customerlist = mapper.Map<List<CustomerVM>>(list);
            return customerlist;
        }

        public async Task<IList<Customer>> GetAllCustomers()
        {
           var result = await db.Customers.ToListAsync();
            return  result;
        }
    }
}
