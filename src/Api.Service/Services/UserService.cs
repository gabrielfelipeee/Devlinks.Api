using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> _repository;
        private readonly IMapper _mapper;
        public UserService(IRepository<UserEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var entities = await _repository.SelectAllAsync();
            var dto = _mapper.Map<IEnumerable<UserDto>>(entities);
            return dto;
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var entity = await _repository.SelectByIdAsync(id);
            var dto = _mapper.Map<UserDto>(entity);
            return dto;
        }

        public async Task<UserDtoCreateResult> PostAsync(UserDtoCreate user)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            entity.Password = passwordHash;
            var result = await _repository.InsertAsync(entity);

            var dto = _mapper.Map<UserDtoCreateResult>(result);
            return dto;
        }

        public async Task<UserDtoUpdateResult> PutAsync(UserDtoUpdate user)
        {
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);

            var result = await _repository.UpdateAsync(entity);
            var dto = _mapper.Map<UserDtoUpdateResult>(result);
            return dto;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
