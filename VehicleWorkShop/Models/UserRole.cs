using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleWorkShop.Models
{
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Users User { get; set; }
        public virtual Role Role { get; set; }

    }
}
