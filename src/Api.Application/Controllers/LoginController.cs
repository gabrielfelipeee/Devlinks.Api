using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [SwaggerOperation("Realiza o login do usuário. Não requer autenticação.")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto user)
        {
            var result = await _loginService.FindUserByLogin(user);
            if (result != null && !result.Authenticated)
                return Unauthorized(result);

            return Ok(result);
        }
    }
}
