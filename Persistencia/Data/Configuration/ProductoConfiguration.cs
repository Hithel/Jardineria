

using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
        {
            public void Configure(EntityTypeBuilder<Producto> builder)
            {
                builder.ToTable("producto");
    
                builder.HasKey(p => p.CodigoProducto);

                builder.Property(p => p.CodigoProducto)
                .HasColumnType("Varchar")
                .HasMaxLength(15);

                builder.Property(p => p.Nombre)
                .HasColumnType("varchar")
                .HasMaxLength(70)
                .IsRequired();

                builder.HasOne(p => p.GamaProducto)
                .WithMany(p => p.Productos)
                .HasForeignKey(p => p.Gama)
                .IsRequired();

                builder.Property(p => p.Dimensiones)
                .HasColumnType("varchar")
                .HasMaxLength(25);

                builder.Property(p => p.Proveedor)
                .HasColumnType("varchar")
                .HasMaxLength(50);

                builder.Property(p => p.Descripcion)
                .HasColumnType("Text");

                builder.Property(p => p.CantidadStock)
                .HasColumnType("Smallint")
                .HasMaxLength(6)
                .IsRequired();

                builder.Property(p => p.PrecioVenta)
                .HasColumnType("decimal")
                .HasPrecision(15,2)
                .IsRequired();

                builder.Property(p => p.PrecioProveedor)
                .HasColumnType("decimal")
                .HasPrecision(15,2);
            }
        }