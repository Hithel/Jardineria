

using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class OficinaConfiguration : IEntityTypeConfiguration<Oficina>
        {
            public void Configure(EntityTypeBuilder<Oficina> builder)
            {
                builder.ToTable("oficina");

                builder.HasKey(p => p.CodigoOficina);

                builder.Property(p => p.CodigoOficina)
                .HasColumnType("varchar")
                .HasMaxLength(10);

                builder.Property(p => p.Cuidad)
                .HasColumnType("varchar")
                .HasMaxLength(30)
                .IsRequired();

                builder.Property(p => p.Pais)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(p => p.Region)
                .HasColumnType("varchar")
                .HasMaxLength(50);

                builder.Property(p => p.CodigoPostal)
                .HasColumnType("varchar")
                .HasMaxLength(10)
                .IsRequired();

                builder.Property(p => p.Telefono)
                .HasColumnType("varchar")
                .HasMaxLength(20)
                .IsRequired();

                builder.Property(p => p.LineaDireccion1)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(p => p.LineaDireccion2)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            }
        }