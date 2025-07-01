using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VehicleWorkShop.ViewModels
{
    public class PaymentVM
    {
        public int PaymentId { get; set; }
        public int SaleId { get; set; }
        public int CustomerId { get; set; }
        public int PaymentTypeId { get; set; }
        public string? AccNo { get; set; } 
        public string? BankName { get; set; } 
        public TimeSpan Time { get; set; }

        public List<SelectListItem> Customers { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> PaymentType { get; set; } = new List<SelectListItem>();
    }
}
