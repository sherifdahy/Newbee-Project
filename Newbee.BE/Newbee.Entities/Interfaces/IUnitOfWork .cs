using Newbee.Entities.Models;
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
        int Save();
    }
}
