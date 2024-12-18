using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Api.Service.Shared;
using Api.Service.Shared.Exceptions;

namespace Api.Service.Services.BusinessRules
{
    public class UserBusinessRules
    {
        private readonly GetAuthenticatedUserId _getAuthenticatedUserId;
        private readonly IUserRepository _userRepository;
        public UserBusinessRules(GetAuthenticatedUserId getAuthenticatedUserId, IUserRepository userRepository)
        {
            _getAuthenticatedUserId = getAuthenticatedUserId;
            _userRepository = userRepository;
        }

        // Verifica se o usuário existe
        private async Task<UserEntity> EnsureUserExistsAsync(Guid id)
        {
            var user = await _userRepository.SelectByIdAsync(id);
            if (user == null)
                throw new NotFoundException("Usuário não encontrado.");
            return user;
        }

        public async Task ValidateUserCreateAsync(string email)
        {
            if (await _userRepository.SelectByEmailAsync(email) != null)
                throw new ConflictException("O email informado já está registrado no sistema.");
        }

        public async Task ValidateUserUpdateAsync(UserDtoUpdate userDto)
        {
            var user = await EnsureUserExistsAsync(userDto.Id);

            // Id do usuário autenticado
            var authenticatedUserId = await _getAuthenticatedUserId.AuthenticatedUserId();

            if (user.Id != authenticatedUserId)
                throw new ForbiddenAccessException("Você não tem permissão para editar dados de outro usuário.");

            if (await _userRepository.SelectByEmailAsync(userDto.Email) != null && user.Email != userDto.Email)
                throw new ConflictException("O email informado já está registrado no sistema.");

            var userSlug = await _userRepository.SelectBySlugAsync(userDto.Slug);
            if(userSlug != null && userSlug.Id != authenticatedUserId)
                throw new ConflictException("O slug informado já está em uso por outro usuário.");
        }

        public async Task ValidateUserDeleteAsync(Guid id)
        {
            var existingUser = await EnsureUserExistsAsync(id);

            // Id do usuário autenticado
            var authenticatedUserId = await _getAuthenticatedUserId.AuthenticatedUserId();

            if (existingUser.Id != authenticatedUserId)
                throw new ForbiddenAccessException("Você não tem permissão para excluir este usuário");
        }
    }
}
