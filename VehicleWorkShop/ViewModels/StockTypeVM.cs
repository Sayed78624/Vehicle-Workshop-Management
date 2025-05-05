using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.ViewModels
{
    public class StockTypeVM
    {
        [Key]
        public int StockTypeId { get; set; }
        [Required,Display(Name ="StockType Name")]
        [StringLength(100)]
        public string StockTypeName { get; set; }
    }
}
