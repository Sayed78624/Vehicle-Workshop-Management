using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.ViewModels
{
    public class CategoryVM
    {
        [Key]
        public int CategoryId { get; set; }
        [Required, Display(Name ="Category Name")]
        [StringLength(100)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
