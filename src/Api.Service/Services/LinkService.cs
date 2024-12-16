using Api.Domain.Dtos.Link;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services;
using Api.Domain.Models;
using Api.Domain.Repository;
using Api.Service.Services.BusinessRules;
using Api.Service.Shared;
using AutoMapper;

namespace Api.Service.Services
{
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository _linkRepository;
        private readonly IMapper _mapper;
        private readonly LinkBusinessRules _linkBusinessRules;
        private readonly GetAuthenticatedUserId _getAuthenticatedUserId;
        private EntityFluentValidationService<LinkDtoCreate, LinkDtoUpdate> _entityFluentValidationService;

        public LinkService(
            ILinkRepository linkRepository,
            IMapper mapper,
            LinkBusinessRules linkBusinessRules,
            GetAuthenticatedUserId getAuthenticatedUserId,
            EntityFluentValidationService<LinkDtoCreate, LinkDtoUpdate> entityFluentValidationService)
        {
            _linkRepository = linkRepository;
            _mapper = mapper;
            _linkBusinessRules = linkBusinessRules;
            _getAuthenticatedUserId = getAuthenticatedUserId;
            _entityFluentValidationService = entityFluentValidationService;
        }

        public async Task<IEnumerable<LinkDto>> GetAllAsync()
        {
            var entities = await _linkRepository.SelectAllAsync();
            var dto = _mapper.Map<IEnumerable<LinkDto>>(entities);
            return dto;
        }

        public async Task<LinkDto> GetByIdAsync(Guid id)
        {
            // Verifica se o link pertence ao usuário autenticado
            await _linkBusinessRules.EnsureLinkBelongsToAuthenticatedUserAsync(id);

            var entity = await _linkRepository.SelectByIdAsync(id);
            var dto = _mapper.Map<LinkDto>(entity);
            return dto;
        }
        public async Task<IEnumerable<LinkDto>> GetByUserAuthenticatedAsync()
        {
            var authenticatedUserId = await _getAuthenticatedUserId.AuthenticatedUserId(); // Id do usuário autenticado

            var entities = await _linkRepository.SelectByUserAuthenticated(authenticatedUserId);
            var dto = _mapper.Map<IEnumerable<LinkDto>>(entities);
            return dto;
        }
        public async Task<LinkDtoCreateResult> PostAsync(LinkDtoCreate link)
        {
            await _entityFluentValidationService.ValidateCreateAsync(link);

            var authenticatedUserId = await _getAuthenticatedUserId.AuthenticatedUserId(); // Id do usuário autenticado

            var model = _mapper.Map<LinkModel>(link);
            var entity = _mapper.Map<LinkEntity>(model);
            entity.UserId = authenticatedUserId;

            var result = await _linkRepository.InsertAsync(entity);
            var dto = _mapper.Map<LinkDtoCreateResult>(result);
            return dto;
        }
        public async Task<LinkDtoUpdateResult> PutAsync(LinkDtoUpdate link)
        {
            await _entityFluentValidationService.ValidateUpdateAsync(link);

            var authenticatedUserId = await _getAuthenticatedUserId.AuthenticatedUserId(); // Id do usuário autenticado

            // Verifica se o link pertence ao usuário autenticado
            await _linkBusinessRules.EnsureLinkBelongsToAuthenticatedUserAsync(link.Id);

            var model = _mapper.Map<LinkModel>(link);
            var entity = _mapper.Map<LinkEntity>(model);
            entity.UserId = authenticatedUserId;

            var result = await _linkRepository.UpdateAsync(entity);
            var dto = _mapper.Map<LinkDtoUpdateResult>(result);
            return dto;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            await _linkBusinessRules.EnsureLinkBelongsToAuthenticatedUserAsync(id);

            return await _linkRepository.DeleteAsync(id);
        }
    }
}
