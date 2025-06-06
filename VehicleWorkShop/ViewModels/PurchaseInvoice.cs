namespace VehicleWorkShop.ViewModels
{
    public class PurchaseInvoice
    {
        public int InvoiceId { get; set; }
        public string SupplierName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal GrandTotal { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string ManagerName { get; set; }
        public List<PurchaseDetailVM> Details { get; set; }

    }
}
