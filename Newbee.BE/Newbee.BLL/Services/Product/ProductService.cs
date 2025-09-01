
namespace Newbee.BLL.Services;

public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Product>> CreateAsync(Product product)
    {
        await _unitOfWork.Products.AddAsync(product);
        _unitOfWork.Save();

        return Result.Success(product);
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var result = await GetByIdAsync(id);

        if (!result.IsSuccess)
            return Result.Failure<bool>(ProductErrors.NotFound);

        _unitOfWork.Products.Delete(result.Value);
        _unitOfWork.Save();

        return Result.Success(true);
    }

    public async Task<Result<IEnumerable<Product>>> GetAllAsync(int companyId)
    {
        if (companyId == 0)
            return Result.Failure<IEnumerable<Product>>(CompanyErrors.InvalidId);

        if (!_unitOfWork.Companies.IsExist(x => x.Id == companyId))
            return Result.Failure<IEnumerable<Product>>(CompanyErrors.NotFound);
        
        var products = await _unitOfWork.Products.FindAllAsync(x=>x.CompanyId == companyId);

        return Result.Success(products.Adapt<IEnumerable<Product>>());
    }

    public async Task<Result<Product>> GetByIdAsync(int id)
    {
        if (id == 0)
            return Result.Failure<Product>(ProductErrors.InvalidId);

        var product = await _unitOfWork.Products.FindAsync(x => x.Id == id);

        return Result.Success(product.Adapt<Product>());
    }

    public Task<Result<bool>> UpdateAsync(int id, Product product)
    {
        throw new NotImplementedException();
    }
}
