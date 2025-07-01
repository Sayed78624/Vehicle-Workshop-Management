using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Data;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Repository
{
    public class PaymentRP : IPayment
    {
        private readonly WorkShopDbContext db;
        private readonly IMapper mapper;
        public PaymentRP(WorkShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<IActionResult> CreatePayment(PaymentVM paymentVM)
        {
            try
            {
                var payment = new Payment
                {
                    PaymentId = paymentVM.PaymentId,
                    SaleId = paymentVM.SaleId,
                    CustomerId = paymentVM.CustomerId,
                    PaymentTypeId = paymentVM.PaymentTypeId,
                    AccNo = paymentVM.AccNo,
                    BankName = paymentVM.BankName,
                    Time = DateTime.Now.TimeOfDay
                };
                db.Payments.Add(payment);
                await db.SaveChangesAsync();    
                return new OkResult();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return new JsonResult(message);
            }
        }
    }
}
