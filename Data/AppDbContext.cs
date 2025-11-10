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
            base.OnModelCreating(modelBuilder);

            // Users ↔ Roles (n:1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Restaurant ↔ Surveys (1:n)
            modelBuilder.Entity<Survey>()
                .HasOne(s => s.Restaurant)
                .WithMany(r => r.Surveys)
                .HasForeignKey(s => s.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Survey ↔ Checkpoint (n:m) via SurveyCheckpoint mit Composite Key
            modelBuilder.Entity<SurveyCheckpoint>()
                .HasKey(sc => new { sc.SurveyId, sc.CheckpointId });

            modelBuilder.Entity<SurveyCheckpoint>()
                .HasOne(sc => sc.Survey)
                .WithMany(s => s.SurveyCheckpoints)
                .HasForeignKey(sc => sc.SurveyId);

            modelBuilder.Entity<SurveyCheckpoint>()
                .HasOne(sc => sc.Checkpoint)
                .WithMany(c => c.SurveyCheckpoints)
                .HasForeignKey(sc => sc.CheckpointId);

            // Rating → Restaurant, Survey, Checkpoint (alle Pflicht)
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Restaurant)
                .WithMany(x => x.Ratings)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Survey)
                .WithMany()
                .HasForeignKey(r => r.SurveyId);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Checkpoint)
                .WithMany()
                .HasForeignKey(r => r.CheckpointId);

            // (Optional) E-Mail eindeutig
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // (Optional) Rollen-Seed, damit du nichts manuell einfügen musst
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "QM-Manager" },
                new Role { Id = 2, Name = "Restaurant-Manager" }
            );
        }
    }
}

