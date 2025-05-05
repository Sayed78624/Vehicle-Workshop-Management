using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.Models
{
    public class StockType
    {
        [Key]
        public int StockTypeId { get; set; }
        [StringLength(100)]
        public string StockTypeName { get; set; }
    }
}
