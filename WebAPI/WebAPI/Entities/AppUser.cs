using Microsoft.AspNetCore.Identity;

namespace WebAPI.Entities
{
    public class AppUser:IdentityUser
    {
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
