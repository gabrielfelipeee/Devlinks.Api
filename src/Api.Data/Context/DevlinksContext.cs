using Api.Data.Mappings;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;


// DbContext é usado para salvar instancias de entidades no db
namespace Api.Data.Context
{
    public class DevlinksContext : DbContext
    {
        // Isso é necessário para para configurar o contexto do db com opções específicas, como a string de conexão
        public DevlinksContext(DbContextOptions<DevlinksContext> options) : base(options)
        { }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<LinkEntity> Links { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>(new UserMap().Configure);
            modelBuilder.Entity<LinkEntity>(new LinkMap().Configure);
        }
    }
}
