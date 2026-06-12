namespace Stalker.Data;

using Microsoft.EntityFrameworkCore;
using Stalker.Models;

public class AppDbContext : DbContext
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Session> Sessions => Set<Session>();

    protected override void OnConfiguring(DbContextOptionsBuilder
        options)
        => options.UseSqlite("Data Source=stalker.db");

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<Game>()
            .Property(g => g.TotalPlayTime)
            .HasConversion(
                v => v.Ticks,
                v => TimeSpan.FromTicks(v));

        model.Entity<Session>()
            .Ignore(s => s.Duration);
    }
}