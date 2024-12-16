using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Service.Shared;
using Api.Service.Shared.Exceptions;

namespace Api.Service.Services.BusinessRules
{
    public class LinkBusinessRules
    {
        private readonly GetAuthenticatedUserId _getAuthenticatedUserId;
        private readonly IRepository<LinkEntity> _repository;
        public LinkBusinessRules(GetAuthenticatedUserId getAuthenticatedUserId, IRepository<LinkEntity> repository)
        {
            _repository = repository;
            _getAuthenticatedUserId = getAuthenticatedUserId;
        }

        // Regra para garantir que o link pertence ao usuário autenticado
        public async Task EnsureLinkBelongsToAuthenticatedUserAsync(Guid linkId)
        {
            var authenticatedUserId = await _getAuthenticatedUserId.AuthenticatedUserId();

            var result = await _repository.SelectByIdAsync(linkId);
            if (result == null)
                throw new NotFoundException("Link não encontrado.");

            // Verifica se o link pertence ao usuário autenticado
            if (authenticatedUserId != result.UserId)
                throw new ForbiddenAccessException("Este link não pertence ao usuário autenticado.");
        }
    }
}
