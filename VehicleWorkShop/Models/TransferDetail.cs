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
        public string SourceStore { get; set; }
        public string DestinationStore { get; set; }

        public virtual Product Product { get; set; }
        public virtual Transfer Transfer { get; set; }
    }
}
