namespace VehicleWorkShop.ViewModels
{
    public class DashboardVM
    {
        public List<string> ModelNames { get; set; }
        public List<int> ProductCounts { get; set; }

        public List<string> TopProductNames { get; set; }
        public List<int> TopProductQuantities { get; set; }

        public int TotalTransfers { get; set; }
        public int ApprovedTransfers { get; set; }

        public int TotalPurchase { get; set; }
        public int ApprovedPurchase { get; set; }

        public int TotalSales { get; set; }
        public int ApprovedSales { get; set; }
    }
}
