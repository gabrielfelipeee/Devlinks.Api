using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


// IDesignTimeDbContextFactory<DevlinksContext> -> Essa interface é usada para criar instância de DbContext em tempo de design (como durante a criação de migrações)

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<DevlinksContext>
    {

        // Usado para criar uma nova instância de DevlinksContext com uma string definida
        public DevlinksContext CreateDbContext(string[] args)
        {
            string connectionString = "Server=localhost;Port=3306;Database=devlinks;Uid=root;Pwd=14589632@Gg";

            var optionsBuilder = new DbContextOptionsBuilder<DevlinksContext>();


            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            return new DevlinksContext(optionsBuilder.Options);
        }
    }
}
