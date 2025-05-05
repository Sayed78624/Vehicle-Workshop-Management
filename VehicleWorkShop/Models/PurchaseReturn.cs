using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleWorkShop.Models
{
    public class PurchaseReturn
    {
        [Key]
        public int PurchaseReturnId { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public decimal GrandTotal { get; set; }
        public bool IsApprove { get; set; }

        public virtual Supplier Supplier { get; set; }

        public IList<PurchaseReturnDetail> PurchaseReturnDetails { get; set; }

    }
}
