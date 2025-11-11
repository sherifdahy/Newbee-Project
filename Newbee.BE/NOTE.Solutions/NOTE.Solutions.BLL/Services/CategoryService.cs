namespace NOTE.Solutions.BLL.Services;

public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CategoryResponse>> CreateAsync(CategoryRequest request,int branchId, CancellationToken cancellationToken = default)
    {
        var categoryExist = _unitOfWork.Categories.IsExist(x => x.Name.ToLower() == request.Name.ToLower());
        if (categoryExist)
            return Result.Failure<CategoryResponse>(CategoryErrors.Duplicated);
        if(!_unitOfWork.Branches.IsExist(x => x.Id == branchId))
            return Result.Failure<CategoryResponse>(BranchErrors.NotFound);
        var category = request.Adapt<Category>();
        category.BranchId = branchId;
        await _unitOfWork.Categories.AddAsync(category, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(category.Adapt<CategoryResponse>());
    }


    public async Task<Result<IEnumerable<CategoryResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _unitOfWork.Categories.FindAllAsync(
            x => !x.IsDeleted,
            cancellationToken: cancellationToken
        );

        return Result.Success(categories.Adapt<IEnumerable<CategoryResponse>>());
    }


    public async Task<Result<CategoryResponse>> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id, cancellationToken);

        if (category is null || category.IsDeleted)
            return Result.Failure<CategoryResponse>(CategoryErrors.NotFound);

        return Result.Success(category.Adapt<CategoryResponse>());
    }


    public async Task<Result<bool>> UpdateAsync(int id, CategoryRequest request, CancellationToken cancellationToken = default)
    {
        var duplicated = _unitOfWork.Categories.IsExist(x => x.Id != id && x.Name.ToLower() == request.Name.ToLower());
        if (duplicated)
            return Result.Failure<bool>(CategoryErrors.Duplicated);

        var category = await _unitOfWork.Categories.GetByIdAsync(id, cancellationToken);

        if (category is null || category.IsDeleted)
            return Result.Failure<bool>(CategoryErrors.NotFound);

        category.Name = request.Name;

        _unitOfWork.Categories.Update(category);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(true);
    }


    public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 0)
            return Result.Failure<bool>(CategoryErrors.InvalidId);

        var category = await _unitOfWork.Categories.GetByIdAsync(id, cancellationToken);

        if (category is null || category.IsDeleted)
            return Result.Failure<bool>(CategoryErrors.NotFound);

        category.IsDeleted = true;
        _unitOfWork.Categories.Update(category);

        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(true);
    }

    public async Task<Result<bool>> RestoreAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 0)
            return Result.Failure<bool>(CategoryErrors.InvalidId);

        var category = await _unitOfWork.Categories.GetByIdAsync(id, cancellationToken);

        if (category == null)
            return Result.Failure<bool>(CategoryErrors.NotFound);

        category.IsDeleted = false;
        _unitOfWork.Categories.Update(category);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(true);
    }

}
