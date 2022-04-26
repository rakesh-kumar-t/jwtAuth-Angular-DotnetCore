using WebAPI.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.DataContext
{
    public class ApplicationDbContext:IdentityDbContext<AppUser,AppUserRole,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserRole> AppRoles { get; set; }
    }
}
