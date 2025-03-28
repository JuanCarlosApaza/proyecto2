using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Modelo;

namespace Proyecto_Final.Data
{
    public class DbConexion : DbContext
    {
        public DbConexion(DbContextOptions<DbConexion> options) : base(options) { }

        public DbSet<Espacio> Espacio { get; set; }
        public DbSet<Evento> Evento { get; set; }
        public DbSet<Anuncio> Anuncio { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Imagenes> Imagen { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Modulos> Modulos { get; set; }
        public DbSet<RolModulos> RolModulos { get; set; }
        public DbSet<UsuarioRol> UsuarioRol { get; set; }
        public DbSet<Lista> Lista { get; set; }
        public DbSet<Fecha> Fecha { get; set; }
        public DbSet<Boleto> Boleto { get; set; }
        public DbSet<Pago> Pago { get; set; }
        public DbSet<Tarjeta> Tarjeta { get; set; }
        public DbSet<Transferencia> Transferencia { get; set; }
        public DbSet<EspacioUsuario> EspacioUsuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Llamar primero a la configuración base

            // 🔹 Configuración para evitar error de múltiples caminos de cascada
            modelBuilder.Entity<Lista>()
                .HasOne(l => l.usuario)
                .WithMany()
                .HasForeignKey(l => l.idusuario)
                .OnDelete(DeleteBehavior.NoAction); // ❌ Evita el error de SQL Server

            modelBuilder.Entity<Lista>()
                .HasOne(l => l.boleto)
                .WithMany()
                .HasForeignKey(l => l.idboleto)
                .OnDelete(DeleteBehavior.Cascade); // ✅ Permite cascada en Boleto
        }
    }
}
