using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.Models
{
    public class WorkShop
    {
        [Key]
        public int WorkShopId { get; set; }
        public string WorkShopName { get; set; }
        public int NumberOfLevel { get; set; }
        public int NumberOfBay { get; set; }
    }
}
