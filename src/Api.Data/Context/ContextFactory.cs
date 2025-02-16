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
            string connectionString = "postgresql://gabriel:pDI5zVjOu6oMgU8cXnsfyYeLTd9T60ri@dpg-cuoutupu0jms73bideq0-a.oregon-postgres.render.com/db_devlinks";

            optionsBuilder.UseNpgsql(connectionString);

            return new DevlinksContext(optionsBuilder.Options);
        }
    }
}
