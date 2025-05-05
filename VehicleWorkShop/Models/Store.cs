using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.Models
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }
        public string Name { get; set; }
    }
}
