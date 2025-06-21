using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Services;
using GameLibrary.Infrastructure.Base;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Api.Controllers
{
    [ApiController]
    [Route("game")]
    public class GameController(GamesService gamesService) : CustomControllerBase
    {
        [HttpPost("add-game")]
        public async Task<IActionResult> AddGameAsync(AddGameRequest payload)
        {
            await gamesService.AddGameAsync(payload);
            return Ok("Game added successfully!");
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateGameAsync(int id, [FromBody] UpdateGameRequest payload)
        {
            await gamesService.UpdateGameAsync(id, payload);
            return Ok("Game updated successfully");
        }


        [HttpGet("get-games")]
        public async Task<IActionResult> GetGamesAsync()
        {
            return Ok(await gamesService.GetGamesWithInfoAsync());
        }


        [HttpGet("get-games-paginated")]
        public async Task<IActionResult> GetGamesPaginatedAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var paged = await gamesService.GetGamesPaginatedAsync(pageNumber, pageSize);
            return Ok(new
            {
                Games = paged.Games,
                TotalGames = paged.TotalCount
            });
        }

        [HttpGet("get-games-by-{id}")]
        public async Task<IActionResult> GetGameFromIdAsync(int id)
        {
            var result = await gamesService.GetGameFromIdAsync(id);
            return Ok(result);
        }

        [HttpDelete("soft-delete/{id}")]
        public async Task<IActionResult> SoftDeleteGameAsync(int id)
        {
            await gamesService.SoftDeleteGameAsync(id);
            return NoContent();
        }
    }
}
