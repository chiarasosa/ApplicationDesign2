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
        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string directory = Directory.GetCurrentDirectory();

                 IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();

                var connectionString = configuration.GetConnectionString(@"obligatrioDB");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }


    }
}