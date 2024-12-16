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

        [HttpGet("all")]
        public async Task<ActionResult> GetAllLinks()
        {
            var result = await _linkService.GetAllAsync();
            return Ok(result);
        }

        [Authorize(Policy = "Bearer")]
        [HttpGet("authenticated")]
        public async Task<ActionResult> GetByUserAuthenticated()
        {
            var result = await _linkService.GetByUserAuthenticatedAsync();
            return Ok(result);
        }

        // [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "Bearer")]
        [HttpGet("{id}", Name = "GetLinkById")]
        public async Task<ActionResult> GetLinkById(Guid id)
        {
            var result = await _linkService.GetByIdAsync(id);
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
        [HttpPut]
        public async Task<ActionResult> PutLink(LinkDtoUpdate link)
        {
            var result = await _linkService.PutAsync(link);
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
