using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        [StringLength(100)]
        public string SupplierName { get; set; }
        [Required]
        [Phone]
        public string Mobile { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [StringLength(100)]
        public string Manager { get; set; }

    }
}
