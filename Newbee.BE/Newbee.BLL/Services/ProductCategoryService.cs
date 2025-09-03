using Newbee.BLL.DTO.ProductCategory.Responses;
using Newbee.Entities;

namespace Newbee.BLL.Services;
public class ProductCategoryService(IUnitOfWork unitOfWork) : IProductCategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ProductCategoryResponse>> CreateAsync(int companyId, ProductCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var productCategory = request.Adapt<ProductCategory>();

        productCategory.CompanyId = companyId;

        await _unitOfWork.ProductCategories.AddAsync(productCategory);
        await _unitOfWork.SaveAsync();

        return Result.Success(productCategory.Adapt<ProductCategoryResponse>());
    }
    public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id == 0)
            return Result.Failure<bool>(ProductCategoryErrors.InvalidId);

        var productCategory = await _unitOfWork.ProductCategories.GetByIdAsync(id);

        if (productCategory is null)
            return Result.Failure<bool>(ProductCategoryErrors.NotFound);

        _unitOfWork.ProductCategories.Delete(productCategory);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(true);
    }
    public async Task<Result<IEnumerable<ProductCategoryResponse>>> GetAllAsync(int companyId, CancellationToken cancellationToken = default)
    {
        if (companyId == 0)
            return Result.Failure<IEnumerable<ProductCategoryResponse>>(CompanyErrors.InvalidId);

        if (!_unitOfWork.Companies.IsExist(x => x.Id == companyId))
            return Result.Failure<IEnumerable<ProductCategoryResponse>>(CompanyErrors.NotFound);

        var productCategories = await _unitOfWork.ProductCategories.FindAllAsync(x => x.CompanyId == companyId);

        return Result.Success(productCategories.Adapt<IEnumerable<ProductCategoryResponse>>());
    }
    public async Task<Result<ProductCategoryResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id == 0)
            return Result.Failure<ProductCategoryResponse>(CompanyErrors.InvalidId);

        var productCategory = await _unitOfWork.ProductCategories.FindAsync(x => x.Id == id);

        if (productCategory is null)
            return Result.Failure<ProductCategoryResponse>(ProductCategoryErrors.NotFound);

        return Result.Success(productCategory.Adapt<ProductCategoryResponse>());
    }
    public async Task<Result<bool>> UpdateAsync(int id, ProductCategoryRequest request, CancellationToken cancellationToken = default)
    {
        if (id == 0)
            return Result.Failure<bool>(ProductCategoryErrors.InvalidId);

        var productCategory = await _unitOfWork.ProductCategories.GetByIdAsync(id);

        if (productCategory is null)
            return Result.Failure<bool>(ProductCategoryErrors.NotFound);

        request.Adapt(productCategory);

        _unitOfWork.ProductCategories.Update(productCategory);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(true);
    }
}
