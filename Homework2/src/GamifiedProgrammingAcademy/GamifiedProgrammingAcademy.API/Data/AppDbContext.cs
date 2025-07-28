namespace GamifiedProgrammingAcademy.API.Data
{
    using GamifiedProgrammingAcademy.API.Entities;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Challenge> Challenges { get; set; }
    }

}
