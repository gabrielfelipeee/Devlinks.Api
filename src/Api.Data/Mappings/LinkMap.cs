using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mappings
{
       public class LinkMap : IEntityTypeConfiguration<LinkEntity>
       {
              public void Configure(EntityTypeBuilder<LinkEntity> builder)
              {
                     builder.ToTable("links");

                     builder.HasKey(x => x.Id);
                     builder.Property(x => x.Id)
                            .HasColumnName("id")
                            .ValueGeneratedOnAdd();

                     builder.Property(x => x.UserId)
                            .HasColumnName("user_id");

                     builder.Property(x => x.Platform)
                             .HasColumnName("platform")
                             .HasMaxLength(45);

                     builder.Property(x => x.Link)
                            .HasColumnName("link")
                            .HasMaxLength(300);

                     builder.Property(x => x.CreatedAt)
                         .HasColumnName("created_at");

                     builder.Property(x => x.UpdatedAt)
                            .HasColumnName("updated_at");
              }
       }
}
