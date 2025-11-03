using KundenUmfrageTool.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace KundenUmfrageTool.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Restaurant> Restaurants => Set<Restaurant>();
        public DbSet<Survey> Surveys => Set<Survey>();
        public DbSet<Checkpoint> Checkpoints => Set<Checkpoint>();
        public DbSet<SurveyCheckpoint> SurveyCheckpoints => Set<SurveyCheckpoint>();
        public DbSet<Rating> Ratings => Set<Rating>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ein Restaurant kann mehrere Umfragen (Surveys) haben. Restaurant  Survey (1:n)
            modelBuilder.Entity<Survey>()
                .HasOne<Restaurant>()
                .WithMany(r => r.Surveys)
                .HasForeignKey("RestaurantId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

