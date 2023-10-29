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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<CartProduct>()
                .HasKey(cp => new { cp.CartID, cp.ProductID });

            modelBuilder.Entity<PurchaseProduct>()
                .HasKey(pp => new { pp.PurchaseID, pp.ProductID });

            // Relación uno a uno entre User y Cart
            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserID);

            // Relación uno a muchos entre Cart y CartProduct
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartProducts)
                .WithOne(cp => cp.Cart)
                .HasForeignKey(cp => cp.CartID);

            // Relación uno a muchos entre Purchase and PurchaseProduct
            modelBuilder.Entity<Purchase>()
                .HasMany(p => p.PurchasedProducts)
                .WithOne(pp => pp.Purchase)
                .HasForeignKey(pp => pp.PurchaseID);

            // Relación uno a muchos entre User and Purchase
            modelBuilder.Entity<User>()
                .HasMany(u => u.Purchases)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserID);
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