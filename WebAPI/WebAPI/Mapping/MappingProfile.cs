using AutoMapper;
using WebAPI.Entities;
using WebAPI.Models;

namespace WebAPI.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, AppUserModel>();
            CreateMap<AppUserModel,AppUser>();
            CreateMap<AppUserRole, AppUserRoleModel>();
            CreateMap<AppUserRoleModel, AppUserRole>();
        }
    }
}
