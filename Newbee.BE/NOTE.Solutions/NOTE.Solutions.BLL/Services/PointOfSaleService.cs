using Microsoft.AspNetCore.Http;
using NOTE.Solutions.API.Extensions;
using NOTE.Solutions.BLL.Contracts.POS.Requests;
using NOTE.Solutions.BLL.Contracts.POS.Responses;
using NOTE.Solutions.Entities.Entities.Company;

namespace NOTE.Solutions.BLL.Services;

public class PointOfSaleService : IPointOfSaleService
{
    private readonly ICacheService _cacheService;
    private readonly IUnitOfWork _unitOfWork;

    public PointOfSaleService(IUnitOfWork unitOfWork,ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;

    }
    public async Task<Result<PointOfSaleResponse>> CreateAsync(int branchId, PointOfSaleRequest request, CancellationToken cancellationToken = default)
    {
        var branch = await _unitOfWork.Branches.GetByIdAsync(branchId,cancellationToken);

        if (branch is null)
            return Result.Failure<PointOfSaleResponse>(BranchErrors.NotFound);

        if (branch.PointOfSales.Any(x => x.POSSerial == request.POSSerial))
            return Result.Failure<PointOfSaleResponse>(POSErrors.Duplicated);

        var pos = new POS()
        {
            BranchId = branchId,
            POSSerial = request.POSSerial,
            ClientId = request.ClientId,
            ClientSecret = request.ClientSecret,
        };
        
        await _unitOfWork.POSs.AddAsync(pos);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(pos.Adapt<PointOfSaleResponse>());
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var pos = await _unitOfWork.POSs.GetByIdAsync(id,cancellationToken);
        
        if(pos is null)
            return Result.Failure(POSErrors.NotFound);

        pos.IsDeleted = true;

        _unitOfWork.POSs.Update(pos);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<PointOfSaleResponse>>> GetAllAsync(int branchId, CancellationToken cancellationToken = default)
    {
        var pointsOfSale = await _unitOfWork.POSs.FindAllAsync(x => x.BranchId == branchId,null,cancellationToken);

        if (pointsOfSale is null)
            return Result.Failure<IEnumerable<PointOfSaleResponse>>(BranchErrors.NotFound);

        return Result.Success(pointsOfSale.Adapt<IEnumerable<PointOfSaleResponse>>());
    }

    public async Task<Result<PointOfSaleResponse>> GetById(int id, CancellationToken cancellationToken = default)
    {
        var pos = await _unitOfWork.POSs.GetByIdAsync(id, cancellationToken);

        if(pos is null)
            return Result.Failure<PointOfSaleResponse>(POSErrors.NotFound);

        return Result.Success(pos.Adapt<PointOfSaleResponse>());
    }

    public async Task<Result> UpdateAsync(int id,PointOfSaleRequest request, CancellationToken cancellationToken = default)
    {
        var pos = await _unitOfWork.POSs.GetByIdAsync(id,cancellationToken);

        if(pos is null)
            return Result.Failure(POSErrors.NotFound);

        pos.POSSerial = request.POSSerial;
        pos.ClientId = request.ClientId;
        pos.ClientId = request.ClientSecret;

        _unitOfWork.POSs.Update(pos);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
