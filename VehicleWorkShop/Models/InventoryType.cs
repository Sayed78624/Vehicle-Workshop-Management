using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.Models
{
    public class InventoryType
    {
        [Key]
        public int InventoryTypeId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public string Remarks { get; set; }
    }
}
