using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NOTE.Solutions.API.Extensions;
using NOTE.Solutions.Entities.Entities;
using NOTE.Solutions.Entities.Entities.Address;
using NOTE.Solutions.Entities.Entities.Company;
using NOTE.Solutions.Entities.Entities.Employee;
using NOTE.Solutions.Entities.Entities.Identity;
using NOTE.Solutions.Entities.Entities.Manager;
using NOTE.Solutions.Entities.Entities.Order;
using NOTE.Solutions.Entities.Entities.Product;
using NOTE.Solutions.Entities.Entities.Unit;
using NOTE.Solutions.Entities.Extensions;
using System.Linq.Expressions;

namespace NOTE.Solutions.DAL.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,int>
{
    private readonly IHttpContextAccessor _httpContext;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions,IHttpContextAccessor httpContextAccessor) : base(dbContextOptions)
    {
        _httpContext = httpContextAccessor;
    }

    #region Identity
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    #endregion
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<Manager> Managers { get; set; }

    #region Address
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<Governorate> Governorates { get; set; }
    public virtual DbSet<Country> Countries { get; set; }
    #endregion

    #region Company
    public virtual DbSet<Company> Companies { get; set; }
    public virtual DbSet<ActiveCode> ActiveCodes { get; set; }
    public virtual DbSet<Branch> Branches { get; set; }
    #endregion

    #region Product
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductUnit> ProductUnits { get; set; }
    public virtual DbSet<Unit> Units { get; set; }
    #endregion

    #region Order
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderLine> OrderDetails { get; set; }
    public virtual DbSet<POS> POSs { get; set; }

    #endregion
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyAllConfigurations();

        modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
            .ToList()
            .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.ClrType.GetProperty("IsDeleted") != null)
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var filter = Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, "IsDeleted"),
                        Expression.Constant(false)
                    ),
                    parameter
                );
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
            }
        }

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<AuditableEntity>();

        if(entries.Any())
        {
            var currentUserId = _httpContext.HttpContext.User.GetUserId();
            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property(x => x.CreatedById).CurrentValue = currentUserId;
                    entityEntry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property(x => x.UpdatedById).CurrentValue = currentUserId;
                    entityEntry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;

                }
            }
        }



        return base.SaveChangesAsync(cancellationToken);
    }
}
