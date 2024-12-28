using System.ComponentModel.DataAnnotations;

namespace JWTTokenWithRolesRealTime.Models
{
    public class UserRole
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
