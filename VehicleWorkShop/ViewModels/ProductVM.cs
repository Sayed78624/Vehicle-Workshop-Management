using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using VehicleWorkShop.Models;

namespace VehicleWorkShop.ViewModels
{
    public class ProductVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PartNo { get; set; }
        public string? Description { get; set; }

        public IFormFile Image { get; set; }

        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        //public IList<CategoryVM> Categories { get; set; }

    }
}
