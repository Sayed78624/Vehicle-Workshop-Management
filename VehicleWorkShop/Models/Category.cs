using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
