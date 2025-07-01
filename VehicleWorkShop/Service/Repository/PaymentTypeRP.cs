using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleWorkShop.Data;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Repository
{
    public class PaymentTypeRP:IPaymentType
    {
        private readonly WorkShopDbContext db;
        private readonly IMapper mapper;
        public PaymentTypeRP(WorkShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Create(PaymentTypeVM paymentTypeVM)
        {
            try
            {
                PaymentType type = new PaymentType
                {
                    PaymentTypeName = paymentTypeVM.PaymentTypeName
                };
                db.PaymentTypes.Add(type);
                await db.SaveChangesAsync();
                return new JsonResult(new { success = true, message = "Supplier created successfully!" });

            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                return new JsonResult(ErrorMessage);
            }
        }

        public async Task<List<PaymentTypeVM>> GetAll()
        {
            var list = await db.PaymentTypes.ToListAsync();
            var typelist = mapper.Map<List<PaymentTypeVM>>(list);
            return typelist;
        }


        public async Task<IActionResult> Delete(int id)
        {
            var type = db.PaymentTypes.Where(p => p.Id == id).FirstOrDefault();
            if (type != null)
            {
                db.PaymentTypes.Remove(type);
                await db.SaveChangesAsync();
                return new OkResult();
            }
            return new BadRequestResult();
        }

        public async Task<IActionResult> Update(PaymentTypeVM paymentTypeVM)
        {
            try
            {
                var pay = db.PaymentTypes.FirstOrDefault(p => p.Id == paymentTypeVM.Id);
                if (pay != null)
                {
                    pay.Id = paymentTypeVM.Id;
                    pay.PaymentTypeName = paymentTypeVM.PaymentTypeName;
                    db.PaymentTypes.Update(pay);
                    await db.SaveChangesAsync();
                    return new OkResult();
                }
                return new NotFoundResult();

            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                return new JsonResult(ErrorMessage);
            }
        }

        public async Task<PaymentTypeVM> GetId(int id)
        {
            var typeId = await db.PaymentTypes.Where(x => x.Id == id).FirstOrDefaultAsync();
            var data = mapper.Map<PaymentTypeVM>(typeId);
            return data;
        }

        public async Task<IList<PaymentType>> GetAllType()
        {
            var type = await db.PaymentTypes.ToListAsync();
            return type;
        }
    }
}
