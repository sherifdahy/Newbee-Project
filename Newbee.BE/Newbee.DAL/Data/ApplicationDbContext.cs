using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newbee.Entities.Models;
using System.Reflection;


namespace Newbee.DAL.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions):base (dbContextOptions)
    {
        
    }
    public DbSet<Company> Companies { get; set; }
    public DbSet<OTP> OTPs { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<ApplicationUser>(user =>
        {
            user.OwnsMany(u => u.RefreshTokens, rt =>
            {
                rt.ToTable("RefreshTokens");              
                rt.WithOwner().HasForeignKey("UserId");   
                rt.HasKey("Id");                           
            });
        });

        base.OnModelCreating(modelBuilder);
    }

}
