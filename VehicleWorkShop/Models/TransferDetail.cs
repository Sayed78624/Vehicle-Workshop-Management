using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleWorkShop.Models
{
    public class TransferDetail
    {
        [Key]
        public int DetailId { get; set; }
        [ForeignKey(nameof(Transfer))]
        public int Tran_Id { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [ForeignKey(nameof(Store))]
        public int SourceStoreId { get; set; }
        [ForeignKey(nameof(Store))]
        public int DestinationStoreId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Transfer Transfer { get; set; }
        public virtual Store SourceStore { get; set; }    
        public virtual Store DestinationStore { get; set; }
    }
}
