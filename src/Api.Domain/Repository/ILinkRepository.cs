using Api.Domain.Entities;
using Api.Domain.Interfaces;

namespace Api.Domain.Repository
{
    public interface ILinkRepository : IRepository<LinkEntity>
    {
        Task<IEnumerable<LinkEntity>> SelectByUserAuthenticated(Guid idUserAuthenticated);
    }
}
