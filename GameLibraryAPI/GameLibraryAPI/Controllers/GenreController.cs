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

        [HttpGet("get-genres-paginated")]
        public async Task<IActionResult> GetGenresPaginatedAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var paged = await genresService.GetGenresPaginatedAsync(pageNumber, pageSize);
            return Ok(new
            {
                Genres = paged.Genres,
                TotalGenres = paged.TotalCount
            });
        }

        [HttpGet("get-genres-by-{id}")]
        public async Task<IActionResult> GetGenreFromIdAsync(int id)
        {
            var result = await genresService.GetGenreFromIdAsync(id);
            return Ok(result);
        }

        [HttpDelete("soft-delete/{id}")]
        public async Task<IActionResult> SoftDeleteGenreAsync(int id)
        {
            await genresService.SoftDeleteGenreAsync(id);
            return NoContent();
        }
    }
}
