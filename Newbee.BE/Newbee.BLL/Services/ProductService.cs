using Newbee.BLL.DTO.Product.Requests;
using Newbee.BLL.DTO.Product.Responses;
using Newbee.Entities;

namespace Newbee.BLL.Services;

public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ProductResponse>> CreateAsync(ProductRequest request, CancellationToken cancellationToken = default)
    {
        var product = request.Adapt<Product>();

        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(product.Adapt<ProductResponse>());
    }
    public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id == 0)
            return Result.Failure<bool>(ProductErrors.InvalidId);

        var product = await _unitOfWork.Products.GetByIdAsync(id);

        if (product is null)
            return Result.Failure<bool>(ProductErrors.NotFound);

        _unitOfWork.Products.Delete(product);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(true);
    }
    public async Task<Result<IEnumerable<ProductResponse>>> GetAllAsync(int productCategoryId, CancellationToken cancellationToken = default)
    {
        if (productCategoryId == 0)
            return Result.Failure<IEnumerable<ProductResponse>>(CompanyErrors.InvalidId);

        var products = await _unitOfWork.Products.FindAllAsync(x=>x.ProductCategoryId == productCategoryId);

        return Result.Success(products.Adapt<IEnumerable<ProductResponse>>());
    }
    public async Task<Result<ProductResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id == 0)
            return Result.Failure<ProductResponse>(ProductErrors.InvalidId);

        var product = await _unitOfWork.Products.FindAsync(x => x.Id == id);

        return Result.Success(product.Adapt<ProductResponse>());
    }
    public async Task<Result<bool>> UpdateAsync(int id,ProductRequest request, CancellationToken cancellationToken = default)
    {
        if (id == 0)
            return Result.Failure<bool>(ProductErrors.InvalidId);

        var product = await _unitOfWork.Products.GetByIdAsync(id);

        if (product is null)
            return Result.Failure<bool>(ProductErrors.NotFound);

        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(true);
    }
}
