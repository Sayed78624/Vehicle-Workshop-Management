using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VehicleWorkShop.ViewModels
{
    public class SaleVM
    {
        public int SaleId { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Field is required")]
        public int CustomerId { get; set; }
        public decimal GrandTotal { get; set; }
        public bool IsApprove { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public IList<SaleDetailVM> SaleDetails { get; set; } = new List<SaleDetailVM>();
        public List<SelectListItem> Customers { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Products { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Stores { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> WorkShopes { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Models { get; set; } = new List<SelectListItem>();
        public int SaleDetailId { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public decimal Vat { get; set; }

        public decimal SubTotal { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public int StoreId { get; set; }
        public int WorkShopId { get; set; }
        public int BayId { get; set; }
        public int LevelId { get; set; }
        public string Vin { get; set; }
        public string RegisterNo { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int ModelId { get; set; }

        public string ProductName { get; set; } = string.Empty;
    }
}
