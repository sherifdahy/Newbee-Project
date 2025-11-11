using Microsoft.EntityFrameworkCore.Storage;
using NOTE.Solutions.Entities.Entities.Address;
using NOTE.Solutions.Entities.Entities.Company;
using NOTE.Solutions.Entities.Entities.Employee;
using NOTE.Solutions.Entities.Entities.Identity;
using NOTE.Solutions.Entities.Entities.Manager;
using NOTE.Solutions.Entities.Entities.Order;
using NOTE.Solutions.Entities.Entities.Product;
using NOTE.Solutions.Entities.Entities.Unit;

namespace NOTE.Solutions.Entities.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public IRepository<Company> Companies { get; }
    public IRepository<Branch> Branches { get; }
    public IRepository<City> Cities { get; }
    public IRepository<Country> Countries { get; }
    public IRepository<Governorate> Governorates { get; }
    public IRepository<ActiveCode> ActiveCodes { get; }
    public IRepository<Unit> Units { get; }
    public IRepository<Product> Products { get; }
    public IRepository<ProductUnit> ProductUnits { get; }
    public IRepository<Order> Orders { get; }
    public IRepository<POS> POSs { get; }
    public IRepository<RefreshToken> RefreshTokens { get; }
    public IRepository<ActiveCodeCompany> ActiveCodeCompanies { get; }
    public IRepository<BranchEmployee> BranchEmployees { get; }
    public IRepository<Employee> Employees { get; }
    public IRepository<Manager> Managers { get; }
    Task<int> SaveAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync();
}
