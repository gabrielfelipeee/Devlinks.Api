using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services;
using Api.Domain.Models;
using Api.Domain.Repository;
using Api.Service.Services.BusinessRules;
using Api.Service.Shared;
using Api.Service.Shared.Exceptions;
using AutoMapper;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly GetAuthenticatedUserId _getAuthenticatedUserId;
        private EntityFluentValidationService<UserDtoCreate, UserDtoUpdate> _entityFluentValidationService;
        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            UserBusinessRules userBusinessRules,
            GetAuthenticatedUserId getAuthenticatedUserId,
            EntityFluentValidationService<UserDtoCreate, UserDtoUpdate> entityFluentValidationService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
            _getAuthenticatedUserId = getAuthenticatedUserId;
            _entityFluentValidationService = entityFluentValidationService;
        }
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var entities = await _userRepository.SelectAllAsync();
            var dto = _mapper.Map<IEnumerable<UserDto>>(entities);
            return dto;
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var entity = await _userRepository.SelectByIdAsync(id)
                ?? throw new NotFoundException("Usuário não encontrado.");

            var dto = _mapper.Map<UserDto>(entity);
            return dto;
        }
        public async Task<UserDto> GetBySlugAsync(string slug)
        {
            var entity = await _userRepository.SelectBySlugAsync(slug)
                    ?? throw new NotFoundException("Usuário não encontrado.");

            var dto = _mapper.Map<UserDto>(entity);
            return dto;
        }
        public async Task<UserDto> GetAuthenticatedUserAsync()
        {
            // Id do usuário autenticado
            var authenticatedUserId = await _getAuthenticatedUserId.AuthenticatedUserId();

            var entity = await _userRepository.SelectUserAuthenticated(authenticatedUserId);
            var dto = _mapper.Map<UserDto>(entity);
            return dto;
        }
        public async Task<UserDtoCreateResult> PostAsync(UserDtoCreate user)
        {
            await _entityFluentValidationService.ValidateCreateAsync(user);

            // Verifica se o email já está registrado no sistema.
            await _userBusinessRules.ValidateUserCreateAsync(user.Email);

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            var model = _mapper.Map<UserModel>(user);
            model.Password = passwordHash;

            var entity = _mapper.Map<UserEntity>(model);
            var result = await _userRepository.InsertAsync(entity);
            var dto = _mapper.Map<UserDtoCreateResult>(result);
            return dto;
        }

        public async Task<UserDtoUpdateResult> PutAsync(Guid id, UserDtoUpdate user)
        {
            await _entityFluentValidationService.ValidateUpdateAsync(user);

            await _userBusinessRules.ValidateUserUpdateAsync(id, user);

            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);

            var result = await _userRepository.UpdateAsync(id, entity);
            var dto = _mapper.Map<UserDtoUpdateResult>(result);
            return dto;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            // Verifica se o usuário sendo excluído é o mesmo que está autenticado.
            await _userBusinessRules.ValidateUserDeleteAsync(id);

            return await _userRepository.DeleteAsync(id);
        }
    }
}
