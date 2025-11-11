using Microsoft.AspNetCore.Http;
using NOTE.Solutions.API.Extensions;
using NOTE.Solutions.Entities.Entities.Product;

namespace NOTE.Solutions.BLL.Services;

public class ProductUnitService : IProductUnitService
{
    private string _cachedKey;
    private int _userId;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    private string[] _includes = new string[]
    {
        nameof(ProductUnit.Product),
        nameof(ProductUnit.Unit)
    };

    public ProductUnitService(IHttpContextAccessor httpContextAccessor,IUnitOfWork unitOfWork,ICacheService cacheService)
    {
        _cacheService = cacheService;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;

        _userId = _httpContextAccessor.HttpContext!.User.GetUserId();
        _cachedKey = $"productUnits_user_{_userId}";
    }

    public async Task<Result> CreateAsync(int productId, ProductUnitRequest request, CancellationToken cancellationToken = default)
    {
        if (productId <= 0)
            return Result.Failure<ProductUnitResponse>(ProductUnitErrors.InvalidId);

        if (!_unitOfWork.Products.IsExist(x => x.Id == productId))
            return Result.Failure<ProductUnitResponse>(ProductErrors.NotFound);

        // Unique constraints checks
        if (_unitOfWork.ProductUnits.IsExist(x => x.ProductId == productId && x.UnitId == request.UnitId))
            return Result.Failure<ProductUnitResponse>(ProductUnitErrors.Duplicated);
        if (_unitOfWork.ProductUnits.IsExist(x => x.ProductId == productId && x.InternalBarcode == request.InternalCode))
            return Result.Failure<ProductUnitResponse>(ProductUnitErrors.DuplicatedInternalCode);

        var productUnit = request.Adapt<ProductUnit>();
        productUnit.ProductId = productId;

        await _unitOfWork.ProductUnits.AddAsync(productUnit, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        await _cacheService.RemoveAsync(_cachedKey);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure(ProductUnitErrors.InvalidId);

        var productUnit = await _unitOfWork.ProductUnits.GetByIdAsync(id, cancellationToken);

        if (productUnit is null)
            return Result.Failure(ProductUnitErrors.NotFound);

        _unitOfWork.ProductUnits.Delete(productUnit);
        await _unitOfWork.SaveAsync(cancellationToken);

        await _cacheService.RemoveAsync(_cachedKey);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<ProductUnitResponse>>> GetAllAsync(int branchId, CancellationToken cancellationToken = default)
    {
        var cachedProductUnits = await _cacheService.GetAsync<IEnumerable<ProductUnitResponse>>(_cachedKey, cancellationToken);

        if (cachedProductUnits is not null)
            return Result.Success(cachedProductUnits);

        var productUnits = await _unitOfWork.ProductUnits.FindAllAsync(x => x.Product!.BranchId == branchId, _includes, cancellationToken);

        await _cacheService.SetAsync(_cachedKey, productUnits.Adapt<IEnumerable<ProductUnitResponse>>(), TimeSpan.FromDays(10), cancellationToken);

        return Result.Success(productUnits.Adapt<IEnumerable<ProductUnitResponse>>());
    }

    public async Task<Result<ProductUnitResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure<ProductUnitResponse>(ProductUnitErrors.InvalidId);

        var productUnit = await _unitOfWork.ProductUnits.FindAsync(x => x.Id == id, null);

        if (productUnit is null)
            return Result.Failure<ProductUnitResponse>(ProductUnitErrors.NotFound);

        return Result.Success(productUnit.Adapt<ProductUnitResponse>());
    }

    public async Task<Result> UpdateAsync(int id, ProductUnitRequest request, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure(ProductUnitErrors.InvalidId);

        var productUnit = await _unitOfWork.ProductUnits.GetByIdAsync(id, cancellationToken);
        if (productUnit is null)
            return Result.Failure(ProductUnitErrors.NotFound);

        // Unique constraints checks (exclude current record)
        if (_unitOfWork.ProductUnits.IsExist(x => x.Id != id && x.ProductId == productUnit.ProductId && x.UnitId == request.UnitId))
            return Result.Failure(ProductUnitErrors.Duplicated);
        if (_unitOfWork.ProductUnits.IsExist(x => x.Id != id && x.ProductId == productUnit.ProductId && x.InternalBarcode == request.InternalCode))
            return Result.Failure(ProductUnitErrors.DuplicatedInternalCode);

        request.Adapt(productUnit);

        _unitOfWork.ProductUnits.Update(productUnit);
        await _unitOfWork.SaveAsync(cancellationToken);

        await _cacheService.RemoveAsync(_cachedKey);

        return Result.Success();
    }
}