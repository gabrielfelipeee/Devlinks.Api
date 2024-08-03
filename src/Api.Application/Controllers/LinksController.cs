using System.Net;
using System.Security.Claims;
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

        //  [Authorize(Policy = "Bearer")]
        // [Authorize(Policy = "AdminPolicy")]
        [HttpGet("all")]
        public async Task<ActionResult> GetAllLinks()
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var result = await _linkService.GetAllAsync();
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Authorize(Policy = "Bearer")]
        [HttpGet("authenticated")]
        public async Task<ActionResult> GetByUserAuthenticated()
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var userIdAuthenticated = GetIdUserAuthenticated();
                if (userIdAuthenticated == null) return Unauthorized();

                var result = await _linkService.GetByUserAuthenticatedAsync(userIdAuthenticated.Value);
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "Bearer")]
        [HttpGet]
        [Route("{id}", Name = "GetLinkById")]
        public async Task<ActionResult> GetLinkById(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var userIdAuthenticated = GetIdUserAuthenticated();
                if (userIdAuthenticated == null) return Unauthorized();

                LinkDto result = await _linkService.GetByIdAsync(id, userIdAuthenticated.Value);
                if (result == null) return Ok(new List<LinkDto>());
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Authorize(Policy = "Bearer")]
        [HttpPost]
        public async Task<ActionResult> PostLink(LinkDtoCreate link)
        {
            if (!ModelState.IsValid) return Unauthorized();
            try
            {
                var userIdAuthenticated = GetIdUserAuthenticated();
                if (userIdAuthenticated == null) return Unauthorized();

                var result = await _linkService.PostAsync(link, userIdAuthenticated.Value);
                var location = new Uri(Url.Link("GetLinkById", new { id = result.Id }));
                return Created(location, result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Authorize(Policy = "Bearer")]
        [HttpPut]
        public async Task<ActionResult> PutLink(LinkDtoUpdate link)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var userIdAuthenticated = GetIdUserAuthenticated();
                if (userIdAuthenticated == null) return Unauthorized();

                var result = await _linkService.PutAsync(link, userIdAuthenticated.Value);
                if (result == null) return Forbid();
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Authorize(Policy = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLink(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var userIdAuthenticated = GetIdUserAuthenticated();
                if (userIdAuthenticated == null) return Unauthorized();

                var result = await _linkService.DeleteAsync(id, userIdAuthenticated.Value);
                if (result == false) return Forbid();
                return NoContent();
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        private Guid? GetIdUserAuthenticated()
        {
            // Id do user autenticado
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Tentando converter uma string em Guid
            // Se a conversão for bem sucedida, o valor será atribuído à variável userId
            if (Guid.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }
            return null;
        }
    }
}
