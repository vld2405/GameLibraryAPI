using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Services;
using GameLibrary.Infrastructure.Base;

namespace GameLibrary.Api.Controllers
{

    // !!!!!! jocurile soft deleted apar in alte tabele !!!!!
    // TODO: MIDDLEWARE
    // TODO: GAMES trebuie sa aiba si getter filtrat
    
    [ApiController]
    [Route("developer")]
    [Authorize]
    public class DeveloperController(DevelopersService devsService) : CustomControllerBase
    {
        [HttpPost("add-developer")]
        public async Task<IActionResult> AddDeveloperAsync([FromBody] AddDeveloperRequest payload)
        {
            await devsService.AddDevAsync(payload);
            return Ok("Developer added successfully");
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateDeveloperAsync(int id, [FromBody] UpdateDeveloperRequest payload)
        {
            await devsService.UpdateDeveloperAsync(id, payload);
            return Ok("Developer updated successfully");
        }


        [HttpGet("get-developers")]
        public async Task<IActionResult> GetDevelopersAsync()
        {
            var result = await devsService.GetDevsAsync();
            return Ok(result);
        }

        [HttpGet("get-developers-paginated")]
        public async Task<IActionResult> GetDevelopersPaginatedAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var paged = await devsService.GetDevsPaginatedAsync(pageNumber, pageSize);
            return Ok(new
            {
                Developers = paged.Developers,
                TotalDevs = paged.TotalCount
            });
        }

        [HttpGet("get-developers-filtered")]
        public async Task<IActionResult> GetDeveloperFilteredAsync([FromQuery] string? name = null, [FromQuery] string? sortOrder = "asc")
        {
            var result = await devsService.GetDeveloperFilteredAsync(name, sortOrder);
            return Ok(result);
        }

        [HttpGet("get-developers-by-{id}")]
        public async Task<IActionResult> GetDeveloperFromIdAsync(int id)
        {
            var result = await devsService.GetDeveloperFromIdAsync(id);
            return Ok(result);
        }

        [HttpDelete("soft-delete/{id}")]
        public async Task<IActionResult> SoftDeleteDeveloperAsync(int id)
        {
            await devsService.SoftDeleteDeveloperAsync(id);
            return NoContent();
        }
    }
}