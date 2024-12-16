using Api.Domain.Entities;
using Api.Domain.Interfaces;

namespace Api.Domain.Repository
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<UserEntity> SelectByEmailAsync(string email);
        Task<UserEntity> SelectBySlugAsync(string slug);
        Task<UserEntity> SelectUserAuthenticated(Guid userIdAuthenticated);
    }
}
