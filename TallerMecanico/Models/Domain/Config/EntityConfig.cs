using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TallerMecanico.Models.Domain.Entities;

namespace TallerMecanico.Models.Domain.Config
{
    public class EntityConfig
    {
        public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
        {
            public void Configure(EntityTypeBuilder<Usuario> builder)
            {
                builder.HasKey(e => e.UsuarioId);// configuro llave primaria
                builder.Property(s => s.Nombre).HasColumnType("varchar(100)").HasColumnName("Nombre");
                builder.Property(s => s.Apellido).HasColumnType("varchar(100)").HasColumnName("Apellido");
                builder.Property(s => s.Correo).HasColumnType("varchar(100)").HasColumnName("Correo");
                builder.Property(s => s.Password).HasColumnType("varchar(100)").HasColumnName("Password");
                builder.Property(s => s.recovery).HasColumnType("varchar(255)").HasColumnName("Recovery_token");
                builder.HasMany(a => a.Vehiculos).WithOne(a => a.Usuario).HasForeignKey(c => c.UsuarioId);//configuro llave foranea
                builder.HasMany(a => a.VehiculoMecanicos).WithOne(a => a.Usuario).HasForeignKey(c => c.UsuarioId);//configuro llave foranea
            }
        }

        public class RolConfig : IEntityTypeConfiguration<Rol>
        {
            public void Configure(EntityTypeBuilder<Rol> builder)
            {
                builder.HasKey(e => e.Id);// configuro llave primaria
                builder.Property(s => s.Descripcion).HasColumnType("varchar(25)").HasColumnName("Descripcion");
                builder.Property(s => s.Descripcion2).HasColumnType("varchar(25)").HasColumnName("Descripcion2");
                builder.HasMany(a => a.ModulosRoles).WithOne(a => a.Rol).HasForeignKey(c => c.RolId);//configuro llave foranea
                builder.HasMany(a => a.Usuarios).WithOne(a => a.Rol).HasForeignKey(c => c.RolId);//configuro llave foranea
            }

        }

        public class ModuloConfig : IEntityTypeConfiguration<Modulo>
        {
            public void Configure(EntityTypeBuilder<Modulo> builder)
            {
                builder.HasKey(e => e.Id);// configuro llave primaria
                builder.Property(s => s.Nombre).HasColumnType("varchar(25)").HasColumnName("Nombre");
                builder.Property(s => s.Metodo).HasColumnType("varchar(25)").HasColumnName("Metodo");
                builder.Property(s => s.Controller).HasColumnType("varchar(25)").HasColumnName("Controller");
                builder.HasMany(a => a.ModulosRoles).WithOne(a => a.Modulo).HasForeignKey(c => c.ModuloId);//configuro llave foranea

            }

        }

        public class ModulosRolesConfig : IEntityTypeConfiguration<ModulosRoles>
        {
            public void Configure(EntityTypeBuilder<ModulosRoles> builder)
            {
                builder.HasKey(e => e.Id);// configuro llave primaria
            }

        }

        public class AgrupadoModulosConfig : IEntityTypeConfiguration<AgrupadoModulos>
        {
            public void Configure(EntityTypeBuilder<AgrupadoModulos> builder)
            {
                builder.HasKey(e => e.Id);// configuro llave primaria
                builder.Property(s => s.Descripcion).HasColumnType("varchar(25)").HasColumnName("Descripcion");
                builder.HasMany(a => a.Modulos).WithOne(a => a.AgrupadoModulos).HasForeignKey(c => c.AgrupadoModulosId);//configuro llave foranea
            }

        }

        public class EstadoConfig : IEntityTypeConfiguration<Estado>
        {
            public void Configure(EntityTypeBuilder<Estado> builder)
            {
                builder.HasKey(e => e.idEstado);
                builder.Property(s => s.Nombre).HasColumnType("varchar(25)").HasColumnName("Nombre");
                builder.HasMany(a => a.VehiculoMecanicos).WithOne(a => a.Estado).HasForeignKey(c => c.EstadoId);//configuro llave foranea
            }
        }
        public class MarcaConfig : IEntityTypeConfiguration<Marca>
        {
            public void Configure(EntityTypeBuilder<Marca> builder)
            {
                builder.HasKey(e => e.MarcaId);// configuro llave primaria
                builder.Property(s => s.Nombre).HasColumnType("varchar(25)").HasColumnName("Nombre");
                builder.HasMany(a => a.Modelos).WithOne(a => a.Marca).HasForeignKey(c => c.MarcaId);//configuro llave foranea
            }
        }

        public class ModeloConfig : IEntityTypeConfiguration<Modelo>
        {
            public void Configure(EntityTypeBuilder<Modelo> builder)
            {
                builder.HasKey(e => e.ModeloId);// configuro llave primaria
                builder.Property(s => s.Nombre).HasColumnType("varchar(25)").HasColumnName("Nombre");
                builder.HasMany(a => a.Vehiculos).WithOne(a => a.Modelo).HasForeignKey(c => c.ModeloId);//configuro llave foranea
            }
        }

        public class ColorConfig : IEntityTypeConfiguration<Color>
        {
            public void Configure(EntityTypeBuilder<Color> builder)
            {
                builder.HasKey(e => e.ColorId);// configuro llave primaria
                builder.Property(s => s.Nombre).HasColumnType("varchar(25)").HasColumnName("Nombre");
                builder.HasMany(a => a.Vehiculos).WithOne(a => a.Color).HasForeignKey(c => c.ColorId);//configuro llave foranea
            }
        }

        public class VehiculoConfig : IEntityTypeConfiguration<Vehiculo>
        {
            public void Configure(EntityTypeBuilder<Vehiculo> builder)
            {
                builder.HasKey(e => e.VehiculoId);// configuro llave primaria
            }
        }

        public class VehiculoMecanicoConfig : IEntityTypeConfiguration<VehiculoMecanico>
        {
            public void Configure(EntityTypeBuilder<VehiculoMecanico> builder)
            {
                builder.HasKey(e => e.VehiculoMecanicoId);// configuro llave primaria
            }
        }
    }
}
