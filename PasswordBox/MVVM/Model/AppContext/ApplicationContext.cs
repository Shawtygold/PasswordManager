using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PasswordBox.MVVM.Model.Entities;
using System.IO;

namespace PasswordBox.MVVM.Model.AppContext
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Password> Passwords { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public ApplicationContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();

            optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Something
        }
    }
}
