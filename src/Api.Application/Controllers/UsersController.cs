using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [SwaggerOperation("Retorna uma lista de todos os usuários cadastrados. Requer permissão de Administrador.")]
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllAsync();
            return Ok(result);
        }

        [SwaggerOperation("Retorna um usuário específico pelo ID. Não requer autenticação.")]
        [AllowAnonymous]
        [HttpGet("by-id/{id}")]
        public async Task<ActionResult> GetUserById(Guid id)
        {
            var result = await _userService.GetByIdAsync(id);
            return Ok(result);
        }

        [SwaggerOperation("Retorna um usuário específico pelo slug. Não requer autenticação.")]
        [AllowAnonymous]
        [HttpGet("by-slug/{slug}")]
        public async Task<ActionResult> GetBySlugAsync(string slug)
        {
            var result = await _userService.GetBySlugAsync(slug);
            return Ok(result);
        }

        [SwaggerOperation("Retorna os dados do usuário autenticado. Requer autenticação.")]
        [Authorize(Policy = "Bearer")]
        [HttpGet("authenticated")]
        public async Task<ActionResult> GetUserAuthenticated()
        {
            var result = await _userService.GetAuthenticatedUserAsync();
            return Ok(result);
        }

        [SwaggerOperation("Cria um novo usuário. Não requer autenticação.")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> PostUser(UserDtoCreate user)
        {
            var result = await _userService.PostAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
        }

        [SwaggerOperation("Atualiza os dados de um usuário existente. Requer autenticação.")]
        [Authorize(Policy = "Bearer")]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(Guid id, UserDtoUpdate user)
        {
            var result = await _userService.PutAsync(id, user);
            return Ok(result);
        }

        [SwaggerOperation("Exclui um usuário pelo ID. Requer autenticação.")]
        [Authorize(Policy = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
