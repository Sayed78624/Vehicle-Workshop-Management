namespace VehicleWorkShop.ViewModels
{
    public class TransferInvoiceVM
    {
        public int InvoiceId { get; set; }
        public string Description { get; set; }
        public List<TransferDetailVM> Details { get; set; } 
    }
}
