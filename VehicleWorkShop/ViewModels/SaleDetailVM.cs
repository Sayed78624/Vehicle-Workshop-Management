using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.ViewModels
{
    public class SaleDetailVM
    {
        [Key]
        public int SaleDetailsId { get; set; }
        [ForeignKey("Sale")]
        public int SaleId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Vat { get; set; }
        public decimal SubTotal { get; set; }
        [ForeignKey("Store")]
        public int StoreId { get; set; }
        [ForeignKey("WorkShop")]
        public int? WorkShopId { get; set; }
        public int? BayId { get; set; }
        public int? LevelId { get; set; }
        [StringLength(50)]
        public string? Vin { get; set; }
        [StringLength(50)]
        public string? RegisterNo { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
    }
}
