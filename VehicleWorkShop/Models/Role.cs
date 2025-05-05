using System.ComponentModel.DataAnnotations;

namespace VehicleWorkShop.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [StringLength(100)]
        public string RoleName { get; set; }

    }
}
