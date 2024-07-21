using Api.Domain.Dtos.Link;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            // Users
            CreateMap<UserEntity, UserDto>()
                .ReverseMap();
            CreateMap<UserEntity, UserDtoCreateResult>()
                .ReverseMap();
            CreateMap<UserEntity, UserDtoUpdateResult>()
                .ReverseMap();

            // Links
            CreateMap<LinkEntity, LinkDto>()
                .ReverseMap();
            CreateMap<LinkEntity, LinkDtoCreateResult>()
                .ReverseMap();
            CreateMap<LinkEntity, LinkDtoUpdateResult>()
                .ReverseMap();
        }
    }
}