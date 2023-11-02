using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using VegaStar.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Npgsql;
//using Microsoft.EntityFrameworkCore;
//using Npgsql;
//using Microsoft.OpenApi.Models;

namespace VegaStar
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var connectionString = Configuration.GetConnectionString("PostgresConnection");
            //string connectionStringUrl = Environment.GetEnvironmentVariable("DATABASE_URL");          

            //string connectionString = "Server=localhost;Port=5432;Database=STARVEGA;User Id=postgres;Password=admin;";

            //string.IsNullOrEmpty(connectionStringUrl) ? connectionString : connectionStringUrl;
            NpgsqlConnectionStringBuilder npgsqlConnectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);
            NpgsqlConnectionStringBuilder builder = npgsqlConnectionStringBuilder;

            services.AddDbContext<ApplicationDbContext>
                (options => options.UseNpgsql(builder.ConnectionString));

            services.AddScoped<UserService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dataContext)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Run DB migrationsn on app start
            dataContext.Database.Migrate();
        }

        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public IConfiguration Configuration { get; }

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    // Добавляем сервисы, необходимые для работы с PostgreSQL
        //    services.AddDbContext<ApplicationDbContext>(options =>
        //        options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        //    // Добавляем сервисы MVC
        //    services.AddControllers();

        //    // Добавляем поддержку Swagger
        //    services.AddSwaggerGen(c =>
        //    {
        //        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
        //    });
        //}

        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();

        //        // Включаем поддержку Swagger только в режиме разработки
        //        app.UseSwagger();
        //        app.UseSwaggerUI(c =>
        //        {
        //            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
        //        });
        //    }
        //    else
        //    {
        //        app.UseExceptionHandler("/Home/Error");
        //        app.UseHsts();
        //    }

        //    app.UseHttpsRedirection();
        //    app.UseRouting();
        //    app.UseAuthorization();
        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllers();
        //    });
        //}



        //////////////////////////////////////////////////////////////////////
        //public class ApplicationDbContext : DbContext
        //{
        //    public DbSet<User> Users { get; set; }
        //    public DbSet<UserGroup> UserGroups { get; set; }
        //    public DbSet<UserState> UserStates { get; set; }
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

        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    }
}

