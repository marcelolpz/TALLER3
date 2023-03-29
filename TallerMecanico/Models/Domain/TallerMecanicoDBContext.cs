using Microsoft.EntityFrameworkCore;
using TallerMecanico.Models.Domain.Entities;
using static TallerMecanico.Models.Domain.Config.EntityConfig;

namespace TallerMecanico.Models.Domain
{
    public class TallerMecanicoDBContext : DbContext
    {
        public TallerMecanicoDBContext(DbContextOptions<TallerMecanicoDBContext> options) : base(options) { }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Rol> Rol { get; set; } 
        public DbSet<Modulo> Modulo { get; set; }
        public DbSet<AgrupadoModulos> AgrupadoModulos { get; set; }
        public DbSet<ModulosRoles> ModulosRoles { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Modelo> Modelo { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Vehiculo> Vehiculo { get; set; }
        public DbSet<VehiculoMecanico> VehiculoMecanico { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TallerMecanicoDBContext).Assembly);
            modelBuilder.ApplyConfiguration(new UsuarioConfig());
            modelBuilder.ApplyConfiguration(new RolConfig());
            modelBuilder.ApplyConfiguration(new ModuloConfig());
            modelBuilder.ApplyConfiguration(new AgrupadoModulosConfig());
            modelBuilder.ApplyConfiguration(new ModulosRolesConfig());
            modelBuilder.ApplyConfiguration(new EstadoConfig());
            modelBuilder.ApplyConfiguration(new MarcaConfig());
            modelBuilder.ApplyConfiguration(new ModeloConfig());
            modelBuilder.ApplyConfiguration(new ColorConfig());
            modelBuilder.ApplyConfiguration(new VehiculoConfig());
            modelBuilder.ApplyConfiguration(new VehiculoMecanicoConfig());
        }
    }
}
