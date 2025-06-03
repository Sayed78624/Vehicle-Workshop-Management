using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.Models
{
    public class VehicleModel
    {
        [Key]
        public int ModelId { get; set; }
        public string ModelName { get; set; }
    }
}
