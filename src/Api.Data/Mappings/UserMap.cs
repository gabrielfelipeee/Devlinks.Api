using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mappings
{
       public class UserMap : IEntityTypeConfiguration<UserEntity>
       {
              public void Configure(EntityTypeBuilder<UserEntity> builder)
              {
                     builder.ToTable("users");

                     builder.HasKey(x => x.Id);
                     builder.Property(x => x.Id)
                            .HasColumnName("id")
                            .ValueGeneratedOnAdd();

                     builder.Property(x => x.Name)
                            .HasColumnName("name")
                            .HasMaxLength(100);

                     builder.HasIndex(x => x.Email).IsUnique();
                     builder.Property(x => x.Email)
                            .HasMaxLength(100)
                            .HasColumnName("email");

                     builder.Property(x => x.Password)
                            .HasColumnName("password_hash")
                            .HasMaxLength(60);

                     builder.Property(x => x.Avatar)
                            .HasMaxLength(300)
                            .HasColumnName("avatar");

                     builder.HasIndex(x => x.Slug).IsUnique();
                     builder.Property(x => x.Slug)
                            .HasMaxLength(50)
                            .HasColumnName("slug");

                     builder.Property(x => x.CreatedAt)
                            .HasColumnName("created_at");

                     builder.Property(x => x.UpdatedAt)
                            .HasColumnName("updated_at");

                     // Configuração do relacionamento um-para-muitos
                     builder.HasMany<LinkEntity>() // Define que a entidade UserEntity tem muitos LinkEntity
                            .WithOne() // Define que para cada link existe uma única entidade UserEntity
                            .HasForeignKey(x => x.UserId); // Define qual propriedade em LinkEntity aponta para UserEntity
              }
       }
}
