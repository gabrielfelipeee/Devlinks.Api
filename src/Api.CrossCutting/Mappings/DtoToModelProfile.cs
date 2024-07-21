using Api.Domain.Dtos.Link;
using Api.Domain.Dtos.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            // Users
            CreateMap<UserModel, UserDto>()
                .ReverseMap();
            CreateMap<UserModel, UserDtoCreate>()
                .ReverseMap();
            CreateMap<UserModel, UserDtoUpdate>()
                .ReverseMap();

            // Links
            CreateMap<LinkModel, LinkDto>()
                .ReverseMap();
            CreateMap<LinkModel, LinkDtoCreate>()
                .ReverseMap();
            CreateMap<LinkModel, LinkDtoUpdate>()
                .ReverseMap();
        }
    }
}
