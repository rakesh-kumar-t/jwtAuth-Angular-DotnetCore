using Microsoft.AspNetCore.Identity;

namespace WebAPI.Models
{
    public class AppUserModel:IdentityUser
    {
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
