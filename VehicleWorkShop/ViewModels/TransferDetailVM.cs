using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.ViewModels
{
    public class TransferDetailVM
    {
        public int DetailId { get; set; }
        public int Tran_Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Source Store is required")]

        public int SourceStoreId { get; set; }

        [Required(ErrorMessage = "Desination Store is required")]

        public int DestinationStoreId { get; set; }

        public string ProductName { get; set; } = string.Empty;
        public string SourceStoreName {  get; set; } = string.Empty;
        public string DestinationStoreName { get; set;} = string.Empty;
    }
}
