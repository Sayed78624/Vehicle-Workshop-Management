using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.Models
{
    public class Ledger
    {
        [Key]
        public int LedgerId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("InventoryType")]
        public int InventoryTypeId { get; set; }
        [ForeignKey("Users")]
        public int? UserId { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("StockType")]
        public int StockTypeId { get; set; }
        [ForeignKey("Store")]
        public int StoreId { get; set; }
        public virtual Product Product { get; set; }
        public virtual InventoryType InventoryType { get; set; }
        public virtual Users User { get; set; } 
        public virtual StockType StockType { get; set; }
        public virtual Store Store { get; set; }
    }
}
