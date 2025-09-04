using Newbee.Entities.Models;

namespace Newbee.Entities.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public IRepository<Company> Companies { get; }
    public IRepository<ApplicationUser> Users { get; }
    public IRepository<OTP> OTPs { get; }
    public IRepository<Product> Products { get; }
    public IRepository<Customer> Customers { get; }
    public IRepository<ProductCategory> ProductCategories { get; }
    public IRepository<Platform> Platforms { get; }
    public IRepository<Country> Countries { get; }
    public IRepository<City> Cities { get; }
    public IRepository<Zone> Zones { get; }
    Task<int> SaveAsync(CancellationToken cancellationToken = default);
    Task<TResult> ExecuteInTransactionAsync<TResult>(Func<Task<TResult>> action);
}
