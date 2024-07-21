using Api.Domain.Dtos.Link;

namespace Api.Domain.Interfaces.Services
{
    public interface ILinkService
    {
        Task<IEnumerable<LinkDto>> GetAllAsync();
        Task<LinkDto> GetByIdAsync(Guid id, Guid userIdAuthenticated);
        Task<IEnumerable<LinkDto>> GetByUserAuthenticatedAsync(Guid userIdAuthenticated);
        Task<LinkDtoCreateResult> PostAsync(LinkDtoCreate link, Guid userIdAuthenticated);
        Task<LinkDtoUpdateResult> PutAsync(LinkDtoUpdate link, Guid userIdAuthenticated);
        Task<bool> DeleteAsync(Guid id, Guid userIdAuthenticated);
    }
}
