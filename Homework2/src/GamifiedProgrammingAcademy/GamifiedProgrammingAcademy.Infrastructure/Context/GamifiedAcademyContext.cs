using Microsoft.EntityFrameworkCore;
using GamifiedProgrammingAcademy.Domain.Entities;

namespace GamifiedProgrammingAcademy.Infrastructure.Context
{
    public class GamifiedAcademyContext : DbContext
    {
        public GamifiedAcademyContext(DbContextOptions<GamifiedAcademyContext> options) : base(options)
        {
        }

        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<UserChallenge> UserChallenges { get; set; }
        public DbSet<UserBadge> UserBadges { get; set; }
        public DbSet<LeaderboardEntry> LeaderboardEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración básica de Challenge
            modelBuilder.Entity<Challenge>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.Points).IsRequired();
            });

            // Configuración básica de User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}