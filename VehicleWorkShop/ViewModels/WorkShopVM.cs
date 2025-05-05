using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.ViewModels
{
    public class WorkShopVM
    {
        [Key]
        public int WorkShopId { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkShopName { get; set; }
        public int NumberOfLevel { get; set; }
        public int NumberOfBay { get; set; }
    }
}
