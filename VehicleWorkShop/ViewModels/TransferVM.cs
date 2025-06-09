using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace VehicleWorkShop.ViewModels
{
    public class TransferVM
    {
        [Key]
        public int Tran_Id { get; set; }
        public string Description { get; set; }
        public bool IsApprove { get; set; }


        public virtual IList<TransferDetailVM> Details { get; set; } = new List<TransferDetailVM>();
        public List<SelectListItem> Products { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> SourceStores { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> DestinationStores { get; set; } = new List<SelectListItem>();

    }
}
