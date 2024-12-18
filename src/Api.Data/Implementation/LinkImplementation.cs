using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementation
{
    public class LinkImplementation : BaseRepository<LinkEntity>, ILinkRepository
    {
        private DbSet<LinkEntity> _dataset;
        public LinkImplementation(DevlinksContext myContext) : base(myContext)
        {
            _dataset = myContext.Set<LinkEntity>();
        }

        public async Task<IEnumerable<LinkEntity>> SelectByUserAuthenticated(Guid idUserAuthenticated)
        {
            return await _dataset
                        .AsNoTracking()  // Desativa o rastreamento e otimiza a leitura
                        .Where(x => x.UserId == idUserAuthenticated)
                        .ToListAsync();
        }
        public async Task<IEnumerable<LinkEntity>> SelectByUserIdAsync(Guid userId)
        {
            return await _dataset
                        .AsNoTracking() 
                        .Where(x => x.UserId == userId)
                        .ToListAsync();
        }
    }
}
