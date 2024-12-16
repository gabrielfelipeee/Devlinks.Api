using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Api.Service.Shared
{
    public class GetAuthenticatedUserId
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetAuthenticatedUserId(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Id do usuário autenticado
        public async Task<Guid> AuthenticatedUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                throw new UnauthorizedAccessException("Usuário não autenticado.");

            return Guid.Parse(userIdClaim.Value);
        }
    }
}
