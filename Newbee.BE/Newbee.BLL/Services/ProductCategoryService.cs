using Newbee.Entities;

namespace Newbee.BLL.Services;
public class ProductCategoryService(IUnitOfWork unitOfWork) : IProductCategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ProductCategory>> CreateAsync(int companyId, ProductCategory productCategory, CancellationToken cancellationToken = default)
    {
        productCategory.CompanyId = companyId;

        await _unitOfWork.ProductCategories.AddAsync(productCategory);
        await _unitOfWork.SaveAsync();

        return Result.Success(productCategory);
    }
    public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await GetByIdAsync(id);

        if (!result.IsSuccess)
            return Result.Failure<bool>(result.Error);

        _unitOfWork.ProductCategories.Delete(result.Value);
        await _unitOfWork.SaveAsync();

        return Result.Success(true);
    }
    public async Task<Result<IEnumerable<ProductCategory>>> GetAllAsync(int companyId, CancellationToken cancellationToken = default)
    {
        if (companyId == 0)
            return Result.Failure<IEnumerable<ProductCategory>>(CompanyErrors.InvalidId);

        if (!_unitOfWork.Companies.IsExist(x => x.Id == companyId))
            return Result.Failure<IEnumerable<ProductCategory>>(CompanyErrors.NotFound);

        var productCategories = await _unitOfWork.ProductCategories.FindAllAsync(x => x.CompanyId == companyId);

        return Result.Success(productCategories);
    }
    public async Task<Result<ProductCategory>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id == 0)
            return Result.Failure<ProductCategory>(CompanyErrors.InvalidId);

        var productCategory = await _unitOfWork.ProductCategories.FindAsync(x => x.Id == id);

        if (productCategory is null)
            return Result.Failure<ProductCategory>(ProductCategoryErrors.NotFound);

        return Result.Success(productCategory);
    }
    public async Task<Result<bool>> UpdateAsync(int id, ProductCategory productCategory, CancellationToken cancellationToken = default)
    {
        if (id == 0)
            return Result.Failure<bool>(CompanyErrors.InvalidId);

        var result = await GetByIdAsync(id);

        if (!result.IsSuccess)
            return Result.Failure<bool>(result.Error);

        productCategory.Adapt(result.Value);

        _unitOfWork.ProductCategories.Update(result.Value);
        await _unitOfWork.SaveAsync();

        return Result.Success(true);
    }
}
