using WebAPI.Models;
using WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Constants;
using WebAPI.Configuration;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Email or password is empty" });
            else 
            {
                var result = await _userService.Login(user);
                if (result != null)
                {
                    return Ok(new { status = StatusCodes.Status200OK, success = true, data = result.Token });
                }
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Something went wrong" });
            }
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(AppUserModel userModel)
        {
            userModel.Id = Guid.NewGuid().ToString();
            var result = await _userService.Register(userModel, null);
            if (result)
            {
                return Ok(new { status = StatusCodes.Status200OK, success = true, data = "Registered Successfully" });
            }
            return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "Something went wrong" });
        }
        [AuthorizeRoles(UserRoles.Admin)]
        [HttpGet("Hello")]
        public IActionResult Hello()
        {
            return Ok("Hello");
        }
        
    }
}
