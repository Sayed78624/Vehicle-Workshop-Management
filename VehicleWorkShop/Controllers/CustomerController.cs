using Microsoft.AspNetCore.Mvc;
using VehicleWorkShop.Models;
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
        public async Task<IActionResult> Index(string searchTerm = "", int page = 1)
        {
            int pageSize = 7;
            var data =await customer.GetAll();
            if(!string.IsNullOrWhiteSpace(searchTerm))
            {
                data = data.Where(c => c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            int totalItems = data.Count();
            var pagedData = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchTerm = searchTerm;

            return View(pagedData);
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
