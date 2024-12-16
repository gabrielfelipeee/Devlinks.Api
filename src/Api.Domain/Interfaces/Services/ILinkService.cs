using Api.Domain.Dtos.Link;

namespace Api.Domain.Interfaces.Services
{
    public interface ILinkService
    {
        Task<IEnumerable<LinkDto>> GetAllAsync();
        Task<LinkDto> GetByIdAsync(Guid id);
        Task<IEnumerable<LinkDto>> GetByUserAuthenticatedAsync();
        Task<LinkDtoCreateResult> PostAsync(LinkDtoCreate linkDto);
        Task<LinkDtoUpdateResult> PutAsync(LinkDtoUpdate linkDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
