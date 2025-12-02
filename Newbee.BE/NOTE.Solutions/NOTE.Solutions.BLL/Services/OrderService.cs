using Mapster;
using NOTE.Solutions.BLL.Contracts.Document.Requests;
using NOTE.Solutions.BLL.Contracts.Document.Responses;
using NOTE.Solutions.BLL.Errors;
using NOTE.Solutions.BLL.Interfaces;
using NOTE.Solutions.Entities.Entities.Order;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Services;

public class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

   

    public async Task<Result<OrderResponse>> CreateAsync(OrderRequest request, int companyId, CancellationToken cancellationToken = default)
    {
        var order = request.Adapt<Order>();
        order.BranchId = request.PosId;
        //order.CustomerId = request.Customer.IdentificationNumber;  there is no direct mapping for CustomerId in OrderRequest cause Customer_Id is not present

        await _unitOfWork.Orders.AddAsync(order, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        var response = order.Adapt<OrderResponse>();
        return Result.Success(response);
    }

    public async Task<Result<IEnumerable<OrderResponse>>> GetAllAsync(int companyId, CancellationToken cancellationToken = default)
    {
        var orders = await _unitOfWork.Orders.FindAllAsync(x => x.Branch.CompanyId == companyId);
        var response = orders.Adapt<IEnumerable<OrderResponse>>();
        return Result.Success(response);
    }

    public async Task<Result<OrderResponse>> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(id, cancellationToken);

        if (order is null )
            return Result.Failure<OrderResponse>(OrderErrors.NotFound);

        var response = order.Adapt<OrderResponse>();
        return Result.Success(response);
    }

    public async Task<Result<bool>> UpdateAsync(int id, OrderRequest request, CancellationToken cancellationToken = default)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(id, cancellationToken);

        if (order is null)
            return Result.Failure<bool>(OrderErrors.NotFound);

        order.BranchId = request.PosId;
        //order.CustomerId = request.Customer.Id; same as above no direct mapping for CustomerId in OrderRequest
        order.ActiveCodeId = request.ActiveCodeId;
        order.PaymentMethod = request.PaymentMethod;

        _unitOfWork.Orders.Update(order);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(true);
    }

    public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure<bool>(OrderErrors.InvalidId);

        var order = await _unitOfWork.Orders.GetByIdAsync(id, cancellationToken);

        if (order is null)
            return Result.Failure<bool>(OrderErrors.NotFound);

        //order.IsDeleted = true; there is no IsDeleted field in Order entity
        _unitOfWork.Orders.Update(order);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(true);
    }

    public async Task<Result<bool>> RestoreAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure<bool>(OrderErrors.InvalidId);

        var order = await _unitOfWork.Orders.GetByIdAsync(id, cancellationToken);

        if (order is null)
            return Result.Failure<bool>(OrderErrors.NotFound);

        //order.IsDeleted = false;  there is no IsDeleted field in Order entity
        _unitOfWork.Orders.Update(order);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(true);
    }
}