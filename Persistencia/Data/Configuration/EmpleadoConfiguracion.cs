
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
        {
            public void Configure(EntityTypeBuilder<Empleado> builder)
            {
                builder.ToTable("empleado");

                builder.HasKey(p => p.CodigoEmpleado);

                builder.Property(p => p.CodigoEmpleado)
                .HasColumnType("int")
                .HasMaxLength(11);

                builder.Property(p => p.Nombre)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(p => p.Apellido1)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(p => p.Apellido2)
                .HasColumnType("varchar")
                .HasMaxLength(50);

                builder.Property(p => p.Extension)
                .HasColumnType("varchar")
                .HasMaxLength(10)
                .IsRequired();

                builder.Property(p => p.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

                builder.HasOne(p => p.Oficina)
                .WithMany(p => p.Empleados)
                .HasForeignKey(p => p.CodigoOficina)
                .IsRequired();

                builder.HasOne(p => p.Jefe)
                .WithMany(p => p.Empleados)
                .HasForeignKey(p => p.CodigoJefe);

                builder.Property(p => p.Puesto)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            }
        }