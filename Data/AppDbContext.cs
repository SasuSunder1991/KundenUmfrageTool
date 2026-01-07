using KundenUmfrageTool.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace KundenUmfrageTool.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //-------------------------
        //Tabellen(Dbsets) der Datenbank
        //-----------------------
        public DbSet<User> Users => Set<User>();  // Benutzer
        public DbSet<Role> Roles => Set<Role>();  // Rollen(QM, Manager)
        public DbSet<Restaurant> Restaurants => Set<Restaurant>();  // Restaurants 
        public DbSet<Survey> Surveys => Set<Survey>();   // Umfragen
        public DbSet<Checkpoint> Checkpoints => Set<Checkpoint>();  // Bereichen(Service , Essen....)
        public DbSet<SurveyCheckpoint> SurveyCheckpoints => Set<SurveyCheckpoint>();  //Verknüpfung Umfrage  ↔ Bereich (n:m)
        public DbSet<Rating> Ratings => Set<Rating>();  // kundenbewertungen

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ----------------------------------------------------------
            // 1) User ↔ Role   (n : 1)
            //    Ein Benutzer hat genau EINE Rolle
            //    Eine Rolle kann viele Benutzer haben (z.B. 10 Restaurant-Manager)
            // ----------------------------------------------------------
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)   // User hat eien Rolle
                .WithMany()           //Eine Rolle hat viele user
                .HasForeignKey(u => u.RoleId)  // Verbindung läuft über RoleId
                .OnDelete(DeleteBehavior.Cascade);

            // Cascade = Wenn eine Rolle gelöscht würde, würden alle Benutzer gelöscht.
            // In der Praxis löscht man Rollen NIE -> sicher.



            // ----------------------------------------------------------
            // 2) Restaurant ↔ Survey (1 : n)
            //    Ein Restaurant kann mehrere Umfragen besitzen
            // ----------------------------------------------------------
            modelBuilder.Entity<Survey>()
                .HasOne(s => s.Restaurant)   //Jede Umfrage gehört zu einem Restaurant
                .WithMany(r => r.Surveys)    // Ein Restaurant hat viele Umfragen
                .HasForeignKey(s => s.RestaurantId)   // Verbindung läuft über RestaurantId
                .OnDelete(DeleteBehavior.Cascade);
            // Wenn ein Restaurant gelöscht wird → lösche alle Umfragen dieses Restaurants.



            // ----------------------------------------------------------
            // 3) Survey ↔ Checkpoint (n : m)
            //    Eine Umfrage hat mehrere Bereiche
            //    Ein Bereich kann in mehreren Umfragen verwendet werden
            //    Verknüpfungstabelle SurveyCheckpoint wird als JOIN-Tabelle genutzt
            // ----------------------------------------------------------
            modelBuilder.Entity<SurveyCheckpoint>()
                .HasKey(sc => new { sc.SurveyId, sc.CheckpointId });  // Composite Key = Kombination aus Survey + Checkpoint

            modelBuilder.Entity<SurveyCheckpoint>()
                .HasOne(sc => sc.Survey)
                .WithMany(s => s.SurveyCheckpoints)  // Eine Umfrage hat viele Zuweisungen
                .HasForeignKey(sc => sc.SurveyId);

            modelBuilder.Entity<SurveyCheckpoint>()
                .HasOne(sc => sc.Checkpoint)
                .WithMany(c => c.SurveyCheckpoints)   // Ein Bereich hat viele Zuweisungen
                .HasForeignKey(sc => sc.CheckpointId);

            // ----------------------------------------------------------
            // 4) Rating (Bewertungen) gehören immer zu:
            //    → einem Restaurant
            //    → einer Umfrage
            //    → einem Bereich (Checkpoint)
            // ----------------------------------------------------------
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Restaurant)
                .WithMany(x => x.Ratings)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
            // Wenn Restaurant gelöscht wird → lösche Bewertungen


            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Survey)
                .WithMany()
                .HasForeignKey(r => r.SurveyId);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Checkpoint)
                .WithMany()
                .HasForeignKey(r => r.CheckpointId);

            // ----------------------------------------------------------
            // 5) E-Mail-Adresse muss eindeutig sein
            //    Verhindert doppelte Benutzerregistrierung
            // ----------------------------------------------------------
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            // ----------------------------------------------------------
            // 6) Seed Data (Startdaten)
            //    Standardrollen werden automatisch beim ersten Start angelegt
            // ----------------------------------------------------------
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "QM-Manager" },
                new Role { Id = 2, Name = "Restaurant-Manager" }
            );
        }
    }
}
/* 
- HasData = Startdaten
- HasOne = hat eins
- With Many= hat andere hat viele
- IsUnique = darf nicht doppelt sein
- hasIndex machst eine spalte schneller durchsuchbar
- HarForeignKey sagt, über welche Feld zwei Tabellen miteinander verbunden sind

modelBuilder.Entity<SurveyCheckpoint>()
    .HasKey(sc => new { sc.SurveyId, sc.CheckpointId });  
Warum 2 Felder? (Composite Key)
Weil SurveyCheckpoint eine Zwischentabelle (n:m) ist.
Ein Kombination aus : SurveyId und CheckpointID
                                          

Legt den Primärschlüssel fest.
Primärschlüssel = eindeutige ID einer Zeile.

HasKey legt fest, was die eindeutige ID einer Tabelle ist.
Bei Zwischentabellen (n:m) sind es oft zwei Felder zusammen.




 
 
 
 
 
 
 */
