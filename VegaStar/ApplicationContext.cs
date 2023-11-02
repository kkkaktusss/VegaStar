using System;
using VegaStar.Entity;
using Microsoft.EntityFrameworkCore;
namespace VegaStar
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
        : base(options)
        {

        }
        
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<UserGroup> UserGroups { get; set; } = null!;
        public DbSet<UserState> UserStates { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                //.SetBasePath(Directory.GetDirectoryRoot("/Users/antonnaumov/StudioProjects/VegaStar/VegaStar/appsettingsDb.json"))
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettingsDb.json")
                .Build();

            // string NameConnection = "PostgresConnection";
            string connectionString = configuration.GetConnectionString("PostgresConnection") ?? "Server=localhost;Port=5432;Database=VegaStarTEST;User Id=postgres;Password=admin;";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}

