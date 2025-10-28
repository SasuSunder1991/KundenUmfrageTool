using Microsoft.EntityFrameworkCore;
using KundenUmfrageTool.Api.Models;

namespace KundenUmfrageTool.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AppUser> Users => Set<AppUser>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Restaurant> Restaurants => Set<Restaurant>();
        public DbSet<Survey> Surveys => Set<Survey>();
        public DbSet<Checkpoint> Checkpoints => Set<Checkpoint>();
        public DbSet<SurveyCheckpoint> SurveyCheckpoints => Set<SurveyCheckpoint>();
        public DbSet<Rating> Ratings => Set<Rating>();
    }
   
    }
