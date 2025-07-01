using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Controllers
{
    public class PaymentTypeController : Controller
    {
        private readonly IPaymentType paymentType;
        public PaymentTypeController(IPaymentType paymentType)
        {
            this.paymentType = paymentType;
        }
        public async Task<IActionResult> Index()
        {
            var list = await this.paymentType.GetAll();
            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            PaymentTypeVM paymentTypeVM = new PaymentTypeVM();
            return View(paymentTypeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(PaymentTypeVM paymentTypeVM)
        {
            if (ModelState.IsValid)
            {
                await paymentType.Create(paymentTypeVM);
                return RedirectToAction("Index");
            }
            return View(paymentTypeVM);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var updatepay = await paymentType.GetId(id);
            return View(updatepay);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PaymentTypeVM paymentTypeVM)
        {
            if (ModelState.IsValid)
            {
                await paymentType.Update(paymentTypeVM);
                return RedirectToAction("Index");
            }
            return View(paymentTypeVM);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await paymentType.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
