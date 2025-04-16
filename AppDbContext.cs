using yorokoanime.Models;
using Microsoft.EntityFrameworkCore;

namespace yorokoanime;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Manga> Manga { get; set; }
    public DbSet<Anime> Anime { get; set; }
    public DbSet<UserFavorite> UserFavorites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure entity properties if needed
        modelBuilder.Entity<User>()
            .HasKey(u => u.Username);  // Primary Key

        base.OnModelCreating(modelBuilder);
    }
}