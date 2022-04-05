using Microsoft.EntityFrameworkCore;
using Shopping.Data.Entities;

namespace Shopping.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Category> Categories { get; set; }


        // OnModelCreating, se ejecuta antes de crear la BD.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Se crea un indice sobre la columna name, porque no puede haber dos paises con el mismo nombre.
            // la c, es de configuracion.
            // le estamos diciendo que cree un indice unico sobre el campo name.
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
        }
    }
}
