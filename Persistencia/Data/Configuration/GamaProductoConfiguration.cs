

using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class GamaProductoConfiguration : IEntityTypeConfiguration<GamaProducto>
        {
            public void Configure(EntityTypeBuilder<GamaProducto> builder)
            {
                builder.ToTable("gama_producto");

                builder.HasKey(x => x.Gama);
                
                builder.Property(p => p.Gama)
                .HasColumnName("gama")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(p => p.DescripcionTexto)
                .HasColumnType("TEXT");

                builder.Property(p => p.DescripcionHtml)
                .HasColumnType("TEXT");

                builder.Property(p => p.Imagen)
                .HasColumnType("Varchar")
                .HasMaxLength(256);
    
                
            }
        }