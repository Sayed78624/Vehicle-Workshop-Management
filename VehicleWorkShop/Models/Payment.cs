using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleWorkShop.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        [ForeignKey("Sale")]
        public int SaleId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("PaymentType")]
        public int PaymentTypeId { get; set; }
        public string AccNo { get; set; }
        public string BankName { get; set; }
        public TimeSpan Time { get; set; }

        public virtual Sale Sale { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual PaymentType PaymentType { get; set; }

    }
}
