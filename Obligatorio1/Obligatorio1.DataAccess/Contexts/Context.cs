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
        public virtual DbSet<Purchase>? Purchase { get; set; }
        public virtual DbSet<Product>? Product { get; set; }

        public virtual DbSet<Session>? Sessions { get; set; }
		
        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<User>()
           .Property(u => u.UserID)
           .ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>()
           .Property(p => p.ProductID)
           .ValueGeneratedOnAdd();


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