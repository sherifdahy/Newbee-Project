

namespace Newbee.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Companies = new Repository<Company>(_context);
            OTPs = new Repository<OTP>(_context);
            Users = new Repository<ApplicationUser>(_context);
            Products = new Repository<Product>(_context);
        }
        public IRepository<Company> Companies { get; }
        public IRepository<OTP> OTPs { get; } 
        public IRepository<ApplicationUser> Users { get; }
        public IRepository<Product> Products { get; }

        public void Dispose()
        {
            _context.Dispose();
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
