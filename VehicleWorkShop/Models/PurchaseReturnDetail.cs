using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.Models
{
    public class PurchaseReturnDetail
    {
        [Key]
        public int PurchaseReturnDetailId { get; set; }
        [ForeignKey("Purchase")]
        public int PurchaseId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Vat { get; set; }
        public decimal SubTotal { get; set; }
        [ForeignKey("Store")]
        public int StoreId { get; set; }
        public virtual Purchase Purchase { get; set; }
        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }

    }
}
