using WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.MiddleWares
{
    public class UserAuthMiddleware:IMiddleware
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public UserAuthMiddleware(IConfiguration configuration,IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        public async Task InvokeAsync(HttpContext context,RequestDelegate next)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                AttachAccountToContext(context, token);
            }
            await next(context);
        }

        private void AttachAccountToContext(HttpContext context,string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    //make token expire exactly at token expiration time instead of 5 mins later
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                //attach account to context
            }
            catch
            {

            }
        }
    }
}
