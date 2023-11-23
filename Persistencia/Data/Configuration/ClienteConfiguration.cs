using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
        {
            public void Configure(EntityTypeBuilder<Cliente> builder)
            {
                builder.ToTable("cliente");
    
                builder.HasKey(p => p.CodigoCliente); 

                builder.Property(p => p.CodigoCliente)
                .HasColumnType("int")
                .HasMaxLength(11);

                builder.Property(p => p.NombreCliente)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(p => p.NombreContacto)
                .HasColumnType("varchar")
                .HasMaxLength(30);

                builder.Property(p => p.ApellidoContacto)
                .HasColumnType("varchar")
                .HasMaxLength(30);

                builder.Property(p => p.Telefono)
                .HasColumnType("varchar")
                .HasMaxLength(15)
                .IsRequired();

                builder.Property(p => p.Fax)
                .HasColumnType("varchar")
                .HasMaxLength(15)
                .IsRequired();

                builder.Property(p => p.LineaDireccion1)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(p => p.LineaDireccion2)
                .HasColumnType("varchar")
                .HasMaxLength(50);

                builder.Property(p => p.Cuidad)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(p => p.Region)
                .HasColumnType("varchar")
                .HasMaxLength(50);

                builder.Property(p => p.Pais)
                .HasColumnType("varchar")
                .HasMaxLength(50);

                builder.Property(p => p.CodigoPostal)
                .HasColumnType("varchar")
                .HasMaxLength(10);

                builder.HasOne(p => p.Empleado)
                .WithMany(p => p.Clientes)
                .HasForeignKey(p => p.CodigoEmpleado);

                builder.Property(p => p.LimiteCredito)
                .HasColumnType("decimal")
                .HasPrecision(15,2);


            }
        }