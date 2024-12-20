using Api.Domain.Dtos.Link;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinksController : ControllerBase
    {
        private readonly ILinkService _linkService;
        public LinksController(ILinkService linkService)
        {
            _linkService = linkService;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<ActionResult> GetAllLinks()
        {
            var result = await _linkService.GetAllAsync();
            return Ok(result);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetLinkById(Guid id)
        {
            var result = await _linkService.GetByIdAsync(id);
            return Ok(result);
        }

        [Authorize(Policy = "Bearer")]
        [HttpGet("user/authenticated")]
        public async Task<ActionResult> GetByUserAuthenticated()
        {
            var result = await _linkService.GetByUserAuthenticatedAsync();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("user/{userId}")]
        public async Task<ActionResult> GetByUserId(Guid userId)
        {
            var result = await _linkService.GetByUserIdAsync(userId);
            return Ok(result);
        }

        [Authorize(Policy = "Bearer")]
        [HttpPost]
        public async Task<ActionResult> PostLink(LinkDtoCreate link)
        {
            var result = await _linkService.PostAsync(link);
            return CreatedAtAction(nameof(GetLinkById), new { id = result.Id }, result);
        }

        [Authorize(Policy = "Bearer")]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutLink(Guid id, LinkDtoUpdate link)
        {
            var result = await _linkService.PutAsync(id, link);
            return Ok(result);
        }

        [Authorize(Policy = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLink(Guid id)
        {
            await _linkService.DeleteAsync(id);
            return NoContent();
        }
    }
}
