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
    }
}
