using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shopping.Data.Entities;

namespace Shopping.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }        
        public DbSet<ProductImage> ProductImages { get; set; }


        // OnModelCreating, se ejecuta antes de crear la BD.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Se crea un indice sobre la columna name, porque no puede haber dos paises con el mismo nombre.
            // la c, es de configuracion.
            // le estamos diciendo que cree un indice unico sobre el campo name.
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();

            // Se crea un indice compuesto
            modelBuilder.Entity<State>().HasIndex("Name","CountryId").IsUnique();
            modelBuilder.Entity<City>().HasIndex("Name","StateId").IsUnique();

            modelBuilder.Entity<Product>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<ProductCategory>().HasIndex("ProductId", "CategoryId").IsUnique();

        }
    }
}
