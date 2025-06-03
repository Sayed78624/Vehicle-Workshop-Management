using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.Models
{
    public class Transfer
    {
        [Key]
        public int Tran_Id { get; set; }
        public string Description { get; set; }
        public bool IsApprove { get; set; }
        public virtual IList<TransferDetail> TransferDetails { get; set; }
    }
}
