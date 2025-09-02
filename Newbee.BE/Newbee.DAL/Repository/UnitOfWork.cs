

using System.Threading.Tasks;

namespace Newbee.DAL.Repository
{
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
        }
        public IRepository<Company> Companies { get; }
        public IRepository<OTP> OTPs { get; } 
        public IRepository<ApplicationUser> Users { get; }
        public IRepository<Product> Products { get; }
        public IRepository<Customer> Customers { get; }
        public IRepository<ProductCategory> ProductCategories { get; }
        public IRepository<Platform> Platforms { get; }

        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
