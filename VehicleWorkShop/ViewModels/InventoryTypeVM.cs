using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.ViewModels
{
    public class InventoryTypeVM
    {
        [Key]
        public int InventoryTypeId { get; set; }
        [Required(ErrorMessage ="Inventory Type Name is required"),Display(Name ="InventoryType Name")]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Remarks { get; set; }
    }
}
