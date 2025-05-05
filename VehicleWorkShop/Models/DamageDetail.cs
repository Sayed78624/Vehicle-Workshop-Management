using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleWorkShop.Models
{
    public class DamageDetail
    {
        [Key]
        public int DamageDetailId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Vat { get; set; }
        public decimal SubTotal { get; set; }
        [ForeignKey("Store")]
        public int StoreId { get; set; }
        [ForeignKey("Damage")]
        public int DamageId { get; set; }
        public virtual Damage Damage { get; set; }
        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
