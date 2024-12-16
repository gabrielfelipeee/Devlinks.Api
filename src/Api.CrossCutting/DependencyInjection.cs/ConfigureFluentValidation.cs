using Api.Service.Validators;
using Api.Service.Validators.Link;
using Api.Service.Validators.User;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureFluentValidation
    {
        public static void ConfigureDependenciesFluentValidation(IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<UserCreateValidator>();
            services.AddValidatorsFromAssemblyContaining<UserUpdateValidator>();

            services.AddValidatorsFromAssemblyContaining<LinkCreateValidator>();
            services.AddValidatorsFromAssemblyContaining<LinkUpdateValidator>();

            services.AddValidatorsFromAssemblyContaining<LoginValidator>();
        }
    }
}
