using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllAsync();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("by-id/{id}")]
        public async Task<ActionResult> GetUserById(Guid id)
        {
            var result = await _userService.GetByIdAsync(id);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("by-slug/{slug}")]
        public async Task<ActionResult> GetBySlugAsync(string slug)
        {
            var result = await _userService.GetBySlugAsync(slug);
            return Ok(result);
        }

        [Authorize(Policy = "Bearer")]
        [HttpGet("authenticated")]
        public async Task<ActionResult> GetUserAuthenticated()
        {
            var result = await _userService.GetAuthenticatedUserAsync();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> PostUser(UserDtoCreate user)
        {
            var result = await _userService.PostAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
        }

        [Authorize(Policy = "Bearer")]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(Guid id, UserDtoUpdate user)
        {
            var result = await _userService.PutAsync(id, user);
            return Ok(result);
        }

        [Authorize(Policy = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
