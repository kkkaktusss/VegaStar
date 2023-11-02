using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
//namespace VegaStar
//{
//	public class EntityConnect
//	{


//		public EntityConnect()
//		{
//		}
//	}
//}

//namespace VegaStar.Entity;

//public class ApplicationDbContext : DbContext
//{
//    public DbSet<User> Users { get; set; } = null!;

//    public DbSet<UserGroup> UserGroups { get; set; } = null!;
//    public DbSet<UserState> UserStates { get; set; } = null!;
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        IConfigurationRoot configuration = new ConfigurationBuilder()
//            .SetBasePath(Directory.GetDirectoryRoot("/Users/antonnaumov/StudioProjects/VegaStar/appsettingsDb.json"))
//            .AddJsonFile("appsettingsDb.json")
//            .Build();

//        // string NameConnection = "PostgresConnection";
//        string connectionString = configuration.GetConnectionString("PostgresConnection") ?? "Server=localhost;Port=5432;Database=STARVEGA;User Id=postgres;Password=admin;";
//        optionsBuilder.UseNpgsql(connectionString);
//    }
//}


//////////////////////////////////////////////////////////////////////////////
//public class User
//{
//    public int UserId { get; set; }
//    public string Login { get; set; } = " ";
//    public string Password { get; set; } = " ";
//    public DateTime CreatedDate { get; set; }

//    // Внешний ключ для UserGroup
//    public int UserGroupId { get; set; }
//    public UserGroup UserGroup { get; set ; } 

//    // Внешний ключ для UserState
//    public int UserStateId { get; set; }
//    public UserState UserState { get; set; }
//}

//public class UserGroup
//{
//    public int UserGroupId { get; set; }
//    public string Code { get; set; } = " ";
//    public string Description { get; set; } = " ";

//    // Навигационное свойство, чтобы связать сущность UserGroup с User
//    // public ICollection<User> Users { get; set; }
//}
//public class UserState
//{
//    public int UserStateId { get; set; }
//    public string Code { get; set; } = " ";
//    public string Description { get; set; } = " ";

//    // Навигационное свойство, чтобы связать сущность UserState с User
//    // public ICollection<User> Users { get; set; }
//}


