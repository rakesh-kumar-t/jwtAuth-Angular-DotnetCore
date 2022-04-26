using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Configuration
{
    public class AuthorizeRolesAttribute:AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}
