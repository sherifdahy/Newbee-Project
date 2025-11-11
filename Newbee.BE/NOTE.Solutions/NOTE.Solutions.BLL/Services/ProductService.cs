using NOTE.Solutions.Entities.Entities.Product;

namespace NOTE.Solutions.BLL.Services;

public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ProductResponse>> CreateAsync( int categoryId, ProductRequest request, CancellationToken cancellationToken = default)
    {
        if (!_unitOfWork.Categories.IsExist(x => x.Id == categoryId))
            return Result.Failure<ProductResponse>(CategoryErrors.NotFound);

        if (_unitOfWork.Products.IsExist(x => x.Name == request.Name && x.CategoryId == categoryId))
            return Result.Failure<ProductResponse>(ProductErrors.Duplicated);

        var product = request.Adapt<Product>();
        product.CategoryId = categoryId;

        await _unitOfWork.Products.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(product.Adapt<ProductResponse>());
    }


    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure(ProductErrors.InvalidId);

        var product = await _unitOfWork.Products.GetByIdAsync(id, cancellationToken);

        if (product is null)
            return Result.Failure(ProductErrors.NotFound);

        _unitOfWork.Products.Delete(product);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<ProductResponse>>> GetAllAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        if(!_unitOfWork.Categories.IsExist(x => x.Id == categoryId))
            return Result.Failure<IEnumerable<ProductResponse>>(CategoryErrors.NotFound);

        var products = await _unitOfWork.Products.FindAllAsync(x => x.CategoryId == categoryId, cancellationToken:cancellationToken);
        return Result.Success(products.Adapt<IEnumerable<ProductResponse>>());
    }

    public async Task<Result<ProductResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure<ProductResponse>(ProductErrors.InvalidId);

        var product = await _unitOfWork.Products.FindAsync(x => x.Id == id, null);

        if (product is null)
            return Result.Failure<ProductResponse>(ProductErrors.NotFound);

        return Result.Success(product.Adapt<ProductResponse>());
    }

    public async Task<Result> UpdateAsync(int id, ProductRequest request, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure(ProductErrors.InvalidId);

        if (_unitOfWork.Products.IsExist(x => x.Name == request.Name))
            return Result.Failure(ProductErrors.Duplicated);

        var product = await _unitOfWork.Products.GetByIdAsync(id, cancellationToken);

        if (product is null)
            return Result.Failure(ProductErrors.NotFound);

        request.Adapt(product);

        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}