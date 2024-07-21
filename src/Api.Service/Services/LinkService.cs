using Api.Domain.Dtos.Link;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Service.Services
{
    public class LinkService : ILinkService
    {
        private readonly IRepository<LinkEntity> _repository;

        private readonly IMapper _mapper;
        public LinkService(IRepository<LinkEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<LinkDto>> GetAllAsync()
        {
            var links = await _repository.SelectAllAsync();
            var dto = _mapper.Map<IEnumerable<LinkDto>>(links);
            return dto;
        }

        public async Task<LinkDto> GetByIdAsync(Guid id, Guid userIdAuthenticated)
        {
            var link = await _repository.SelectByIdAsync(id);

            if (userIdAuthenticated != link.UserId) return null;

            var dto = _mapper.Map<LinkDto>(link);
            return dto;
        }

        public async Task<IEnumerable<LinkDto>> GetByUserAuthenticatedAsync(Guid userIdAuthenticated)
        {
            var result = await _repository.SelectAllAsync();

            var linksByUser = result.Where(x => x.UserId == userIdAuthenticated).ToList();
            if (linksByUser == null) return null;

            var dto = _mapper.Map<IEnumerable<LinkDto>>(linksByUser);
            return dto;
        }
        public async Task<LinkDtoCreateResult> PostAsync(LinkDtoCreate link, Guid userIdAuthenticated)
        {
            var model = _mapper.Map<LinkModel>(link);
            var entity = _mapper.Map<LinkEntity>(model);
            entity.UserId = userIdAuthenticated;

            var result = await _repository.InsertAsync(entity);

            var dto = _mapper.Map<LinkDtoCreateResult>(result);
            return dto;
        }

        public async Task<LinkDtoUpdateResult> PutAsync(LinkDtoUpdate link, Guid userIdAuthenticated)
        {
            var resultEntity = await _repository.SelectByIdAsync(link.Id);
            if (userIdAuthenticated != resultEntity.UserId) return null;

            var model = _mapper.Map<LinkModel>(link);
            var entity = _mapper.Map<LinkEntity>(model);
            entity.UserId = userIdAuthenticated;
            
            var result = await _repository.UpdateAsync(entity);
            var dto = _mapper.Map<LinkDtoUpdateResult>(result);
            return dto;
        }
        public async Task<bool> DeleteAsync(Guid id, Guid userIdAuthenticated)
        {
            var entity = await _repository.SelectByIdAsync(id);
            if (userIdAuthenticated != entity.UserId)
            {
                return false;
            }
            return await _repository.DeleteAsync(id);
        }
    }
}
