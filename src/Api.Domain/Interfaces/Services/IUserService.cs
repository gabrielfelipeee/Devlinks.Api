using Api.Domain.Dtos.User;

namespace Api.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(Guid id);
        Task<UserDto> GetBySlugAsync(string slug);
        Task<UserDto> GetAuthenticatedUserAsync();
        Task<UserDtoCreateResult> PostAsync(UserDtoCreate user);
        Task<UserDtoUpdateResult> PutAsync(Guid id, UserDtoUpdate user);
        Task<bool> DeleteAsync(Guid id);
    }
}
