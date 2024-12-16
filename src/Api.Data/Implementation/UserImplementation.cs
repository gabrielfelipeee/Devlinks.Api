using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementation
{
    public class UserImplementation : BaseRepository<UserEntity>, IUserRepository
    {
        private DbSet<UserEntity> _dataset;
        public UserImplementation(DevlinksContext myContext) : base(myContext)
        {
            _dataset = myContext.Set<UserEntity>();
        }

        public async Task<UserEntity> SelectByEmailAsync(string email)
        {
            return await _dataset.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<UserEntity> SelectBySlugAsync(string slug)
        {
            return await _dataset.FirstOrDefaultAsync(x => x.Slug == slug);
        }

        public async Task<UserEntity> SelectUserAuthenticated(Guid userIdAuthenticated)
        {
            return await _dataset.FirstOrDefaultAsync(x => x.Id == userIdAuthenticated);
        }
    }
}
