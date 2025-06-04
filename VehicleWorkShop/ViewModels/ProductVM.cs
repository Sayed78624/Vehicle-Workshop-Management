using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleWorkShop.Models;

namespace VehicleWorkShop.ViewModels
{
    public class ProductVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PartNo { get; set; }
        public string? Description { get; set; }
        public string? ImageName { get; set; }
        public IFormFile Image { get; set; }

        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int ModelId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string ModelName { get; set; } = string.Empty;
        public List<SelectListItem> VehicleModel { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

    }
}
