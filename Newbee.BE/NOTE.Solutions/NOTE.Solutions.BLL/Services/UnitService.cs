using NOTE.Solutions.DAL.Repository;
using NOTE.Solutions.Entities.Entities.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Services;

public class UnitService(IUnitOfWork unitOfWork) : IUnitService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<UnitResponse>> CreateAsync(UnitRequest request, CancellationToken cancellationToken = default)
    {
        if (_unitOfWork.Units.IsExist(x => x.Code.ToLower() == request.Code.ToLower()))
            return Result.Failure<UnitResponse>(UnitErrors.Duplicated);

        var unit = request.Adapt<Unit>();

        await _unitOfWork.Units.AddAsync(unit, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(unit.Adapt<UnitResponse>());
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure(UnitErrors.InvalidId);

        var unit = await _unitOfWork.Units.GetByIdAsync(id, cancellationToken);

        if (unit is null)
            return Result.Failure(UnitErrors.NotFound);

        _unitOfWork.Units.Delete(unit);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<UnitResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var units = await _unitOfWork.Units.GetAllAsync(cancellationToken);

        return Result.Success(units.Adapt<IEnumerable<UnitResponse>>());
    }

    public async Task<Result<UnitResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure<UnitResponse>(UnitErrors.InvalidId);

        var unit = await _unitOfWork.Units.GetByIdAsync(id, cancellationToken);

        if (unit is null)
            return Result.Failure<UnitResponse>(UnitErrors.NotFound);

        return Result.Success(unit.Adapt<UnitResponse>());
    }

    public async Task<Result> UpdateAsync(int id, UnitRequest request, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure<UnitResponse>(UnitErrors.InvalidId);

        var unit = await _unitOfWork.Units.GetByIdAsync(id, cancellationToken);

        if (unit is null)
            return Result.Failure<UnitResponse>(UnitErrors.NotFound);

        request.Adapt(unit);

        _unitOfWork.Units.Update(unit);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
