using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.ViewModels
{
    public class SaleDetailVM
    {
        [Key]
        public int SaleDetailsId { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Vat { get; set; }
        public decimal SubTotal { get; set; }
        public int StoreId { get; set; }
        public int WorkShopId { get; set; }
        public int BayId { get; set; }
        public int LevelId { get; set; }
        public string Vin { get; set; }
        public string RegisterNo { get; set; }
        public int CustomerId { get; set; }
        public int ModelId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ModelName { get; set; } = string.Empty;
        public string WorkShopName { get; set; } = string.Empty;
    }
}
