using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.Models
{
    public class PaymentType
    {
        [Key]
        public int Id { get; set; }
        public string PaymentTypeName { get; set; }
    }
}
