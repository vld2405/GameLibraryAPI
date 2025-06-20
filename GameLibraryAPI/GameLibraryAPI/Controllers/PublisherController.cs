using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Api.Controllers
{
    [ApiController]
    [Route("publisher")]
    public class PublisherController(PublisherService publisherService) : ControllerBase
    {
        [HttpPost("add-publisher")]
        public async Task<IActionResult> AddPublisherAsync([FromBody] AddPublisherRequest payload)
        {
            await publisherService.AddPublisherAsync(payload);
            return Ok("Publisher added successfully");
        }

        [HttpGet("get-publishers")]
        public async Task<IActionResult> GetPublishersAsync()
        {
            return Ok(await publisherService.GetPublishersAsync());
        }

        [HttpGet("get-publishers-paginated")]
        public async Task<IActionResult> GetGenresPaginatedAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var paged = await publisherService.GetPublishersPaginatedAsync(pageNumber, pageSize);
            return Ok(new
            {
                Publishers = paged.Publishers,
                TotalPublishers = paged.TotalCount
            });
        }

        [HttpGet("get-publishers-by-{id}")]
        public async Task<IActionResult> GetGenreFromIdAsync(int id)
        {
            var result = await publisherService.GetPublisherFromIdAsync(id);
            return Ok(result);
        }

        [HttpDelete("soft-delete/{id}")]
        public async Task<IActionResult> SoftDeleteGenreAsync(int id)
        {
            await publisherService.SoftDeletePublisherAsync(id);
            return NoContent();
        }
    }
}
