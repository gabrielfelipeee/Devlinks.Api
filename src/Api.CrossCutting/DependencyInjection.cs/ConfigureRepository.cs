using Api.Data.Context;
using Api.Data.Implementation;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserImplementation>();
            services.AddScoped<ILinkRepository, LinkImplementation>();

            string connectionString = "Server=localhost;Port=3306;Database=devlinks;Uid=root;Pwd=14589632@Gg";

            services.AddDbContext<DevlinksContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
        }
    }
}
