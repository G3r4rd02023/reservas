using Microsoft.EntityFrameworkCore;
using reservas.Data.Entities;

namespace reservas.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Edificio> Edificios { get; set; }
        public DbSet<Aula> Aulas { get; set; }
        public DbSet<Servicio> Servicios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Edificio>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Aula>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Servicio>().HasIndex(c => c.Name).IsUnique();
        }
    }
}
