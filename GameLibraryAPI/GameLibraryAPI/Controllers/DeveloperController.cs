using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Services;

namespace GameLibrary.Api.Controllers
{
    [ApiController]
    [Route("developer")]
    public class DeveloperController(DevelopersService devsService) : ControllerBase
    {
        [HttpPost("add-develoer")]
        public async Task<IActionResult> AddDeveloper([FromBody] AddDeveloperRequest payload)
        {
            await devsService.AddDevAsync(payload);
            return Ok("Developer added successfully");
        }
    }
}