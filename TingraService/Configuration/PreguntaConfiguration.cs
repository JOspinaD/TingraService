using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TingraService.Models;

namespace TingraService.Configuration
{
    public class PreguntaConfiguration : IEntityTypeConfiguration<Pregunta>
    {
        public void Configure(EntityTypeBuilder<Pregunta> builder)
        {
            builder.ToTable(nameof(Pregunta));

            builder.Property(fd => fd.CreatedAt)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(fd => fd.UpdatedAt)
                .IsRequired()
                .ValueGeneratedOnUpdate();

            builder.HasOne(fd => fd.Empresa)
                .WithMany(fd => fd.Pregunta)
                .HasForeignKey(fd => fd.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
