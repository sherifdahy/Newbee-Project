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
    Task<int> SaveAsync(CancellationToken cancellationToken = default);
}
