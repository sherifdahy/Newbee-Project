using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newbee.Entities.Extensions;
using Newbee.Entities.Models;
using Newbee.Entities.Models.Unit;


namespace Newbee.DAL.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser,IdentityRole<int>,int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions):base (dbContextOptions)
    {
    }

    #region DbSets
    public virtual DbSet<OTP> OTPs { get; set; }
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Company> Companies { get; set; }
    public virtual DbSet<Platform> Platforms { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderDetails> OrderDetails { get; set; }
    public virtual DbSet<Payment> Payments { get; set; }
    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public virtual DbSet<Unit> Units { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductUnit> ProductUnits { get; set; }
    public virtual DbSet<ProductUnitImage> ProductUnitImages { get; set; }
    public virtual DbSet<ProductColor> ProductColors { get; set; }
    public virtual DbSet<ProductSize> ProductSizes { get; set; }
    public virtual DbSet<ProductCategory> ProductCategories { get; set; }
    public virtual DbSet<District> Districts { get; set; }
    public virtual DbSet<Zone> Zones { get; set; }
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<Country> Countries { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyAllConfigurations();

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
