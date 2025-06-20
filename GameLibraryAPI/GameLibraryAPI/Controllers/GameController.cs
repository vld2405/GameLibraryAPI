using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Api.Controllers
{
    [ApiController]
    [Route("game")]
    public class GameController(GamesService gamesService) : ControllerBase
    {
        [HttpPost("add-game")]
        public async Task<IActionResult> AddGameAsync(AddGameRequest payload)
        {
            await gamesService.AddGameAsync(payload);
            return Ok("Game added successfully!");
        }


        [HttpGet("get-games")]
        public async Task<IActionResult> GetGamesAsync()
        {
            return Ok(await gamesService.GetGamesWithInfoAsync());
        }
    }
}
