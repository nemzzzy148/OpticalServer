using Microsoft.EntityFrameworkCore;
using OpticalServer.Models;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<Level> Levels { get; set; }
}