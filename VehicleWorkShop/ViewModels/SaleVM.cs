using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.ViewModels
{
    public class SaleVM
    {
        [Key]
        public int SaleId { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public decimal GrandTotal { get; set; }
        public bool IsApprove { get; set; }
        public int? InventoryTypeId { get; set; }
        public int? StockTypeId { get; set; }
        public IList<SaleDetailVM> SaleDetailVMs { get; set; } = new List<SaleDetailVM>();
    }
}
