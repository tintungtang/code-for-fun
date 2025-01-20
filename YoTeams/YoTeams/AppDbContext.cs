using Microsoft.EntityFrameworkCore;
using YoTeams.Models;

namespace YoTeams;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Member> Members { get; set; }
    public DbSet<SocialItem> SocialItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>().HasData(
            Enumerable.Range(1, 30).Select(i => new Member
            {
                Id = i,
                Name = $"Member {i}",
                Role = i % 2 == 0 ? "Developer" : "Designer"
            }).ToArray()
        );
    }
}
