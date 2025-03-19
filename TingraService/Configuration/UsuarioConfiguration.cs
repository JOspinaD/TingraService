using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TingraService.Models;

namespace TingraService.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable(nameof(Usuario));

            builder.Property(fd => fd.CreatedAt)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(fd => fd.UpdatedAt)
                .IsRequired()
                .ValueGeneratedOnUpdate();
        }
    }
}
