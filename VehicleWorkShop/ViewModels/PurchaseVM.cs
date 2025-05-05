using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VehicleWorkShop.Models;

namespace VehicleWorkShop.ViewModels
{
    public class PurchaseVM
    {
        [Key]

        public int PurchaseId { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public decimal GrandTotal { get; set; }
        public bool IsApprove { get; set; }
        public string SupplierName { get; set; }
        public int? InventoryTypeId { get; set; }
        public int? StockTypeId { get; set; }

        public IList<PurchaseDetailVM> PurchaseDetails { get; set; } = new List<PurchaseDetailVM>();
    }
}
