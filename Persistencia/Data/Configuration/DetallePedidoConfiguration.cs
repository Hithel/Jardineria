using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class DetallePedidoConfiguration : IEntityTypeConfiguration<DetallePedido>
        {
            public void Configure(EntityTypeBuilder<DetallePedido> builder)
            {
                builder.ToTable("detalle_pedido");

                builder.HasKey(p => new { p.CodigoPedido, p.CodigoProducto});

                builder.Property(p => p.CodigoPedido)
                .HasColumnType("int")
                .HasMaxLength(11);

                builder.Property(p => p.CodigoProducto)
                .HasColumnType("varchar")
                .HasMaxLength(15);

                builder.HasOne(p => p.Producto)
                .WithMany(p => p.DetallePedidos)
                .HasForeignKey(p => p.CodigoProducto);

                builder.HasOne(p => p.Pedido)
                .WithMany(p => p.DetallePedidos)
                .HasForeignKey(p => p.CodigoPedido);

                builder.Property(p => p.Cantidad)
                .HasColumnType("int")
                .HasMaxLength(11)
                .IsRequired();

                builder.Property(p => p.PrecioUnidad)
                .HasColumnType("decimal")
                .HasPrecision(15,2)
                .IsRequired();

                builder.Property(p => p.NumeroLinea)
                .HasColumnType("smallint")
                .HasMaxLength(6)
                .IsRequired();
    
                
            }
        }