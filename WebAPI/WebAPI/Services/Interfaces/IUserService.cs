using WebAPI.Entities;
using WebAPI.Models;
using System.Threading.Tasks;

namespace WebAPI.Services.Interfaces
{
    public interface IUserService
    {
        public Task<AppUser> Login(UserLoginModel user);
        public Task<bool> Register(AppUserModel user, string roleId);
    }
}
