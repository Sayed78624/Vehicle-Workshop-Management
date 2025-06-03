using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.ViewModels
{
    public class VehicleModelVM
    {
        [Key]
        public int ModelId { get; set; }
        public string ModelName { get; set; }
    }
}
