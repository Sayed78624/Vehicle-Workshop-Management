using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.Models
{
    public class Damage
    {
        [Key]
        public int DamageId { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
     
        public decimal GrandTotal { get; set; }
        public bool IsApprove { get; set; }

        public IList<DamageDetail> DamageDetails { get; set; }

    }
}
