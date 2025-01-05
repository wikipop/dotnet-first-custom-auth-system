using DziegielAdminPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace DziegielAdminPlatform.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<HumanEntity> HumanEntities { get; set; }
    public DbSet<PlatformUser> PlatformUsers { get; set; }
    public DbSet<PlatformSession> PlatformSessions { get; set; }
    public DbSet<PlatformRole> PlatformRoles { get; set; }
    public DbSet<UserRole> UsersRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=app.db");
        base.OnConfiguring(optionsBuilder);
    }
}