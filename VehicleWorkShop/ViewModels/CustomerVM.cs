using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.ViewModels
{
    public class CustomerVM
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
