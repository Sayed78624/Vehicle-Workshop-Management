namespace VehicleWorkShop.ViewModels
{
    public class StoreProductReportVM
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }   
        public List<ProductStockVM> Products { get; set; }

    }
}
