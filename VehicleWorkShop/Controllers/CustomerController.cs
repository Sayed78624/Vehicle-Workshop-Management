using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomer customer;
        public CustomerController(ICustomer customer)
        {
            this.customer = customer;
        }
        public async Task<IActionResult> Index()
        {
            var allCustomers =await customer.GetAll();
            return View(allCustomers);
        }
        [HttpGet]
        public  IActionResult Create()
        {
            CustomerVM customerVM = new CustomerVM();
            return View(customerVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CustomerVM customerVM)
        {
            if(ModelState.IsValid)
            {
                var result = await customer.Create(customerVM);
                return RedirectToAction("Index");

            }
            return View(customerVM);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var allCustomers = await customer.GetAll();
            var customerVM = allCustomers.FirstOrDefault(s => s.CustomerId == id);
            if (customerVM == null)
            {
                return NotFound();
            }
            return View(customerVM);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await customer.Delete(id);
            if (result is OkResult)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
