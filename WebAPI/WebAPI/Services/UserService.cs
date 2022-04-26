using AutoMapper;
using WebAPI.Configuration;
using WebAPI.Constants;
using WebAPI.DataContext;
using WebAPI.Entities;
using WebAPI.Models;
using WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppUserRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly JwtConfig _jwtConfig;
        public UserService(UserManager<AppUser> userManager,RoleManager<AppUserRole> roleManager,ApplicationDbContext context,IMapper mapper,
             IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _mapper = mapper;
            _jwtConfig = optionsMonitor.CurrentValue;
        }
        public async Task<AppUser> Login(UserLoginModel user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return null;
            }
            else
            {
                AppUser appUser=await _userManager.FindByEmailAsync(user.UserName);
                if (appUser == null)
                    appUser = await _userManager.FindByNameAsync(user.UserName);
                if (appUser == null)
                    return null;
                else
                {
                    var res=await _userManager.CheckPasswordAsync(appUser,user.Password);
                    if (res)
                    {
                        var jwtToken =await GenerateJwtToken(appUser);
                        appUser.Token = jwtToken.Token;
                        return appUser;
                    }
                }
            }
            return null;
        }
        public async Task<bool> Register(AppUserModel user, string roleId)
        {
            var entity = _mapper.Map<AppUser>(user);
           
            var result = await _userManager.CreateAsync(entity,entity.Password);
            //await _userManager.AddToRoleAsync(entity, UserRoles.Admin);
            if (result.Succeeded)
            {
                if ((await _userManager.AddToRoleAsync(entity, UserRoles.Admin)).Succeeded)
                    return true;
            }
            return true ;
        }
       
        private async Task<AuthResult> GenerateJwtToken(AppUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var claims = await GetAllValidClaims(user);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtConfig.ValidIssuer,
                Audience = _jwtConfig.ValidAudience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(5), // 5-10 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);


            return new AuthResult()
            {
                Token = jwtToken,
                Success = true,
            };
        }
        private async Task<List<Claim>> GetAllValidClaims(AppUser user)
        {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Getting the claims that we have assigned to the user
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            // Get the user role and add it to the claims
            var UserRolesModels = await _userManager.GetRolesAsync(user);

            foreach (var userRole in UserRolesModels)
            {
                var role = await _roleManager.FindByNameAsync(userRole);

                if (role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));

                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                  
                }
            }

            return claims;
        }
    }
}
