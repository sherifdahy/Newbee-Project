namespace Newbee.Entities.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public IRepository<Company> Companies { get; }
    public IRepository<ApplicationUser> Users { get; }
    public IRepository<OTP> OTPs { get; }
    public IRepository<Product> Products { get; }
    Task<int> SaveAsync();
}
