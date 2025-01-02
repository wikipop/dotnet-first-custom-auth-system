using DziegielAdminPlatform.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;

namespace DziegielAdminPlatform.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<HumanEntity> HumanEntities { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=app.db");
        base.OnConfiguring(optionsBuilder);
    }
}