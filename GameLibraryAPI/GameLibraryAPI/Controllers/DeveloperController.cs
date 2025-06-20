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
        [HttpPost("add-developer")]
        public async Task<IActionResult> AddDeveloperAsync([FromBody] AddDeveloperRequest payload)
        {
            await devsService.AddDevAsync(payload);
            return Ok("Developer added successfully");
        }


        [HttpGet("get-developers")]
        public async Task<IActionResult> GetDevelopersAsync()
        {
            var result = await devsService.GetDevsAsync();
            return Ok(result);
        }
    }
}