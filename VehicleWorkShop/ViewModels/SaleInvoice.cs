namespace VehicleWorkShop.ViewModels
{
    public class SaleInvoice
    {
        public int InvoiceId { get; set; }
        public string CustomerName { get; set; }
        public string Mobile {  get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public decimal GrandTotal { get; set; }
        public string WorkshopName { get; set; }
        public List<SaleDetailVM> Details { get; set; }
    }
}
