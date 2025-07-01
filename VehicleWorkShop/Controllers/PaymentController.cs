using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPayment pay;
        private readonly IPaymentType payType;
        private readonly ICustomer customer;
        private readonly ISale sale;
        public PaymentController(IPayment pay, IPaymentType payType, ICustomer customer, ISale sale)
        {
            this.pay = pay;
            this.payType = payType;
            this.customer = customer;
            this.sale = sale;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create(int saleid)
        {
            var customerid = await sale.GetCustomerNameBySaleId(saleid);
            try
            {
                var paytypelist = await payType.GetAllType() ?? new List<PaymentType>();
                var type = paytypelist.Select(
                      item => new SelectListItem
                      {
                          Value = item.Id.ToString(),
                          Text = item.PaymentTypeName,
                      }).ToList();
                var customerlist = await customer.GetAllCustomers() ?? new List<Customer>();
                var customers = customerlist.Select(item => new SelectListItem
                {
                    Value = item.CustomerId.ToString(),
                    Text = item.Name,
                }).ToList();
                PaymentVM paymentVM = new PaymentVM()
                {
                    SaleId = saleid,
                    CustomerId =customerid
                };
                paymentVM.Customers = customers;
                paymentVM.PaymentType = type;
                return View(paymentVM);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return View(message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create(PaymentVM paymentVM)
        {
            if(paymentVM.PaymentTypeId == 1)
            {
                paymentVM.AccNo = null;
                paymentVM.BankName = null;
            }
            if(!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                var paytypelist = await payType.GetAllType() ?? new List<PaymentType>();
                var type = paytypelist.Select(
                      item => new SelectListItem
                      {
                          Value = item.Id.ToString(),
                          Text = item.PaymentTypeName,
                      }).ToList();
                var customerlist = await customer.GetAllCustomers() ?? new List<Customer>();
                var customers = customerlist.Select(item => new SelectListItem
                {
                    Value = item.CustomerId.ToString(),
                    Text = item.Name,
                }).ToList();
                return View(paymentVM);

            }
            await pay.CreatePayment(paymentVM);
            return RedirectToAction("Index", "Sale");

        }
    }
}
