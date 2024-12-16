using Api.Domain.Dtos.Link;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services;
using Api.Service.Services;
using Api.Service.Services.BusinessRules;
using Api.Service.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILinkService, LinkService>();
            services.AddTransient<ILoginService, LoginService>();


            // Serviços de Validação
            services.AddScoped<EntityFluentValidationService<UserDtoCreate, UserDtoUpdate>>();
            services.AddScoped<EntityFluentValidationService<LinkDtoCreate, LinkDtoUpdate>>();
        


            // Registre o IHttpContextAccessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<GetAuthenticatedUserId>();
            services.AddScoped<LinkBusinessRules>();
            services.AddScoped<UserBusinessRules>();


        }
    }
}
