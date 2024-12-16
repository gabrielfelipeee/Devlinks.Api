using Api.CrossCutting.Mappings;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureAutoMapper
    {
        public static void ConfigureDependenciesAutoMapper(IServiceCollection services)
        {
            var autoMapperConfig = new MapperConfiguration(config =>
                 {
                     config.AddProfile(new DtoToModelProfile());
                     config.AddProfile(new EntityToDtoProfile());
                     config.AddProfile(new ModelToEntityProfile());
                 });
            IMapper mapper = autoMapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
