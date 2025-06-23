using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Services;
using GameLibrary.Infrastructure.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController(UserService userService) : CustomControllerBase
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

        [HttpPatch("add-game-to-user-{id}")]
        public async Task<IActionResult> AddGameToUserAccountAsync(int id, [FromBody] AddGameToUserRequest payload)
        {
            await userService.AddGameToUserLibraryAsync(id, payload);
            return Ok("Game successfully added to user library.");
        }

        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await userService.GetUsersAsync();
            return Ok(users);
        }

    }
}
