using Microsoft.EntityFrameworkCore.Storage;
using NOTE.Solutions.DAL.Data;
using NOTE.Solutions.Entities.Entities.Address;
using NOTE.Solutions.Entities.Entities.Company;
using NOTE.Solutions.Entities.Entities.Employee;
using NOTE.Solutions.Entities.Entities.Identity;
using NOTE.Solutions.Entities.Entities.Manager;
using NOTE.Solutions.Entities.Entities.Order;
using NOTE.Solutions.Entities.Entities.Product;
using NOTE.Solutions.Entities.Entities.Unit;
using NOTE.Solutions.Entities.Interfaces;

namespace NOTE.Solutions.DAL.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;

        Companies = new Repository<Company>(_context);
        Users = new Repository<ApplicationUser>(_context);
        Branches = new Repository<Branch>(_context);  
        Cities = new Repository<City>(_context);  
        Governorates = new Repository<Governorate>(_context);  
        Countries = new Repository<Country>(_context);  
        ActiveCodes = new Repository<ActiveCode>(_context);  
        Units = new Repository<Unit>(_context);  
        Products = new Repository<Product>(_context);  
        ProductUnits = new Repository<ProductUnit>(_context);  
        Orders = new Repository<Order>(_context);
        RefreshTokens = new Repository<RefreshToken>(_context);  
        POSs = new Repository<POS>(_context);
        ActiveCodeCompanies = new Repository<ActiveCodeCompany>(_context);
        BranchEmployees = new Repository<BranchEmployee>(_context);
        Employees = new Repository<Employee>(_context);
        Managers = new Repository<Manager>(_context);
    }

    public IRepository<Company> Companies { get; }
    public IRepository<ActiveCodeCompany> ActiveCodeCompanies { get; }
    public IRepository<ApplicationUser> Users { get; }
    public IRepository<RefreshToken> RefreshTokens { get; }
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
    public IRepository<BranchEmployee> BranchEmployees { get; }
    public IRepository<Employee> Employees { get; }
    public IRepository<Manager> Managers { get; }
    public async Task<IDbContextTransaction> BeginTransactionAsync()
        => await _context.Database.BeginTransactionAsync();
    public void Dispose()
    {
        _context.Dispose();
    }
    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
