using Newbee.Entities.Models;

namespace Newbee.DAL.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;

        Customers = new Repository<Customer>(_context);
        Companies = new Repository<Company>(_context);
        OTPs = new Repository<OTP>(_context);
        Users = new Repository<ApplicationUser>(_context);
        Products = new Repository<Product>(_context);
        ProductCategories = new Repository<ProductCategory>(_context);
        Platforms = new Repository<Platform>(_context);
        Countries = new Repository<Country>(_context);
        Cities = new Repository<City>(_context);
        Zones = new Repository<Zone>(_context);
    }
    public IRepository<Company> Companies { get; }
    public IRepository<OTP> OTPs { get; } 
    public IRepository<ApplicationUser> Users { get; }
    public IRepository<Product> Products { get; }
    public IRepository<Customer> Customers { get; }
    public IRepository<ProductCategory> ProductCategories { get; }
    public IRepository<Platform> Platforms { get; }
    public IRepository<Country> Countries { get; }
    public IRepository<City> Cities { get; }
    public IRepository<Zone> Zones { get; }
    public async Task<TResult> ExecuteInTransactionAsync<TResult>(Func<Task<TResult>> action)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var result = await action();
            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    public void Dispose()
    {
        _context.Dispose();
    }
    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
