using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController(UserService userService) : ControllerBase
    {
        [HttpPost("/register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequest payload)
        {
            await userService.RegisterAsync(payload);
            return Ok("Registration successful");
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest payload)
        {
            var jwtToken = await userService.LoginAsync(payload);

            return Ok(new { token = jwtToken });
        }
    }
}
