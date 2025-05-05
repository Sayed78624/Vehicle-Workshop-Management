using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleWorkShop.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public decimal GrandTotal { get; set; }
        public bool IsApprove { get; set; } 
        public virtual Supplier Supplier { get; set; }
        public IList<PurchaseDetail> PurchaseDetails { get; set; }

    }
}
