
using Microsoft.EntityFrameworkCore;
using MealPlanner.Models;
using System.Data.SqlClient;

namespace MealPlanner.DataSources 
{
    public class MealPlanningContext : DbContext
    {
        private readonly IConfiguration _config;

        public MealPlanningContext(DbContextOptions<MealPlanningContext> options, IConfiguration configuration) : base(options)
        {
            _config = configuration;
        }

        public DbSet<Meal> Meals {get; set;}
        public DbSet<Ingredient> Ingredients {get; set;}
        public DbSet<Plan> Plans { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            var dbUrl = _config["DB_URL"];
            var databaseName = _config["DB_NAME"];
            var user = _config["DB_USER"];
            var password = _config["DB_PASSWORD"];

            var connBuilder = new SqlConnectionStringBuilder();
            connBuilder.UserID = user;
            connBuilder.Password = password;
            connBuilder.DataSource = dbUrl;
            connBuilder.InitialCatalog = databaseName;

            var connectionString = connBuilder.ConnectionString; 
            

            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meal>(x => {
                x.HasKey(m => m.Id);
            });
            modelBuilder.Entity<Ingredient>(x => {
                x.HasKey(i => i.Id);
            });
            modelBuilder.Entity<Plan>(x => x.HasKey(p => p.Id));
        }
    }

}
