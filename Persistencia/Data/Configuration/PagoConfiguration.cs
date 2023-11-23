

using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class PagoConfiguration : IEntityTypeConfiguration<Pago>
        {
            public void Configure(EntityTypeBuilder<Pago> builder)
            {
                builder.ToTable("pago");

                builder.HasOne(p => p.Cliente)
                .WithMany(p => p.Pagos)
                .HasForeignKey(p => p.CodigoCLiente);

                builder.HasOne(p => p.Cliente)
                .WithMany(p => p.Pagos)
                .HasForeignKey(p => p.CodigoCLiente);
    
                builder.Property(p => p.FormaPago)
                .HasColumnType("varchar")
                .HasMaxLength(40)
                .IsRequired();
                
                builder.HasKey(p => p.IdTransacion);

                builder.Property(p => p.IdTransacion)
                .HasColumnType("varchar")
                .HasMaxLength(50);


                builder.Property(p => p.FechaPago)
                .HasColumnType("Date")
                .IsRequired();

                builder.Property(p => p.Total)
                .HasColumnType("decimal")
                .HasPrecision(15,2)
                .IsRequired();

            }
        }