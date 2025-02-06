using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

// Usado para criar as migrações
namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<DevlinksContext>
    {
        public DevlinksContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DevlinksContext>();
            string connectionString = "Server=localhost;Port=3306;Database=devlinks;Uid=root;Pwd=14589632";

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new DevlinksContext(optionsBuilder.Options);
        }
    }
}
