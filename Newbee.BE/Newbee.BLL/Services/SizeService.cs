using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Services;
public class SizeService : ISizeService
{
    public Task<Result<SizeResponse>> CreateAsync(SizeRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<SizeResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<SizeResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateAsync(int id, SizeRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
