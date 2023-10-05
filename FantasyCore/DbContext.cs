using Microsoft.EntityFrameworkCore;
using FantasyCore.Models;

namespace FantasyCore;

public class FantasyContext : DbContext
{
    public DbSet<Sport> Sports { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<Player> Players { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
        .UseNpgsql("Host=localhost;Database=fantasy_db;Username=cameronmackenzie;Password=;")
        .UseSnakeCaseNamingConvention();
}