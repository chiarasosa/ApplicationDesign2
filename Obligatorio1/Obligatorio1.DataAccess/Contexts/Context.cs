using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Obligatorio1.Domain;

namespace Obligatorio1.DataAccess.Contexts
{
    public class Context : DbContext
    {
        public Context() { }

        public Context(DbContextOptions options) : base(options) { }

        public virtual DbSet<User>? Users { get; set; }
        public virtual DbSet<Cart>? Carts { get; set; }
        public virtual DbSet<Purchase>? Purchases { get; set; }
        public virtual DbSet<Product>? Products { get; set; }

        public virtual DbSet<Session>? Sessions { get; set; }
		
        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<User>()
           .Property(u => u.UserID)
           .ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>()
           .Property(p => p.ProductID)
           .ValueGeneratedOnAdd();

            modelBuilder.Entity<Cart>()
           .Property(c => c.CartID)
           .ValueGeneratedOnAdd();

            modelBuilder.Entity<Purchase>()
            .Property(p => p.PurchaseID)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Session>()
            .Property(s => s.SessionID)
            .ValueGeneratedOnAdd();

            /*Relación Uno a Muchos entre User y Purchase:
              Esta relación indica que un usuario puede tener muchas compras, pero cada compra pertenece a un solo usuario. 
              En tu entidad User, ya tienes una propiedad de navegación Purchases. Aquí está cómo puedes configurar esta relación:*/
            modelBuilder.Entity<User>()
            .HasMany(u => u.Purchases)
            .WithOne()
            .HasForeignKey(p => p.UserID);

            /*Relación Uno a Uno entre User y Cart:
              Esta relación indica que un usuario tiene un carrito y que un carrito pertenece a un usuario.
              En tu entidad User, ya tienes una propiedad de navegación Cart. Aquí está cómo puedes configurar esta relación:*/
            modelBuilder.Entity<User>()
            .HasOne(u => u.Cart)
            .WithOne()
            .HasForeignKey<Cart>(c => c.UserID);

            /*Esto establecerá que una compra puede tener varios productos asociados a través de la propiedad PurchasedProducts,
             * sin que la entidad Product contenga una propiedad PurchaseID.*/
            modelBuilder.Entity<Purchase>()
            .HasMany(p => p.PurchasedProducts)
            .WithOne()
            .IsRequired(); // Asegura que la relación sea obligatoria

            modelBuilder.Entity<Purchase>()
             .HasMany(p => p.PurchasedProducts)
             .WithMany();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string directory = Directory.GetCurrentDirectory();

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(directory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("obligatrioDB");

                optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Obligatorio1.DataAccess"));
            }
        }

    }
}