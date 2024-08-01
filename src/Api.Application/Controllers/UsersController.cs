using System.Net;
using System.Security.Claims;
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

        //  [Authorize(Policy = "Bearer")]
        //    [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var result = await _userService.GetAllAsync();
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        //[Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "Bearer")]
        [HttpGet("{id}", Name = "GetUserById")]

        public async Task<ActionResult> GetUserById(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var result = await _userService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }


        [HttpGet("authenticated")]
        public async Task<ActionResult> GetUserAuthenticated()
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                if (GetIdUserAuthenticated() == null)
                {
                    return Forbid();
                }
                var result = await _userService.GetByIdAsync(GetIdUserAuthenticated().Value);
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostUser(UserDtoCreate user)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var result = await _userService.PostAsync(user);
                var location = new Uri(Url.Link("GetUserById", new { id = result.Id }));
                return Created(location, result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Authorize(Policy = "Bearer")]
        [HttpPut]
        public async Task<ActionResult> PutUser(UserDtoUpdate user)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                if (GetIdUserAuthenticated() == null || GetIdUserAuthenticated() != user.Id)
                {
                    return Forbid();
                }

                var result = await _userService.PutAsync(user);
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [Authorize(Policy = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                if (GetIdUserAuthenticated() == null || GetIdUserAuthenticated() != id)
                {
                    return Forbid();
                }
                await _userService.DeleteAsync(id);
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
