using System.ComponentModel.DataAnnotations;

namespace JWTTokenWithRolesRealTime.DTOs
{
    public class LoginDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
