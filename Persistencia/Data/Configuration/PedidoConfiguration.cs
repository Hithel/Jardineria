
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
        {
            public void Configure(EntityTypeBuilder<Pedido> builder)
            {
                builder.ToTable("pedido");
    
                builder.HasKey(p => p.CodigoPedido);

                builder.Property(p => p.CodigoPedido)
                .HasColumnType("int")
                .HasMaxLength(11);

                builder.Property(p => p.FechaPedido)
                .HasColumnType("date")
                .IsRequired();

                builder.Property(p => p.FechaEsperada)
                .HasColumnType("date")
                .IsRequired();

                builder.Property(p => p.FechaEntrega)
                .HasColumnType("date");

                builder.Property(p => p.Estado)
                .HasColumnType("varchar")
                .HasMaxLength(15)
                .IsRequired();

                builder.Property(p => p.Comentarios)
                .HasColumnType("Text");

                builder.HasOne(p => p.Cliente)
                .WithMany(p => p.Pedidos)
                .HasForeignKey(p => p.CodigoCliente)
                .IsRequired();
            }
        }