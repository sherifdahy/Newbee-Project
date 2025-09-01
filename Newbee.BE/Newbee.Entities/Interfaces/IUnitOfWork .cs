using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<Company> Companies { get; }
        public IRepository<ApplicationUser> Users { get; }
        public IRepository<OTP> OTPs { get; }
        public IRepository<Product> Products { get; }
        int Save();
    }
}
