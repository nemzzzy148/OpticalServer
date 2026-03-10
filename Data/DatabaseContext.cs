using Microsoft.EntityFrameworkCore;
using OpticalServer.Functions;
using OpticalServer.Models;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

    public DbSet<User> users { get; set; }
    public DbSet<Level> levels { get; set; }

    public DbSet<LevelReaction> level_reactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<ReactionType>("reaction_type");

        modelBuilder.Entity<LevelReaction>(entity =>
        {
            entity.ToTable("level_reactions");
            entity.HasKey(e => e.ReactionId);
            
            entity.Property(e => e.ReactionType)
                .HasColumnName("reaction");
        }
        );
    }
}