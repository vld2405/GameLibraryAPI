using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Mapping;
using GameLibrary.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Api.Controllers
{
    [ApiController]
    [Route("genre")]
    public class GenreController(GenresService genresService) : ControllerBase
    {
        [HttpPost("add-genre")]
        public async Task<IActionResult> AddGenreAsync(AddGenreRequest payload)
        {
            await genresService.AddGenreAsync(payload);
            return Ok("Genre added successfully!");
        }

        [HttpGet("get-genres")]
        public async Task<IActionResult> GetGenresAsync()
        {
            return Ok(await genresService.GetGenresAsync());
        }
    }
}
