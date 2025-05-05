using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.Models
{
    public class Stock
    {
        [Key]
        public int StockId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ForeignKey("Store")]
        public int StoreId { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }

    }
}
