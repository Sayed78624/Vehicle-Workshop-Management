using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleWorkShop.Models
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public decimal GrandTotal { get; set; }
        public bool IsApprove { get; set; }

        public virtual Customer Customer { get; set; }
        public IList<SaleDetails> SaleDetails { get; set; } 


    }
}
