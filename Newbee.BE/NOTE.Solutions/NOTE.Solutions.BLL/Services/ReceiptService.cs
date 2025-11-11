using Microsoft.AspNetCore.Http;
using NOTE.Solutions.API.Extensions;
using NOTE.Solutions.BLL.Contracts.Document.Requests;
using NOTE.Solutions.BLL.Contracts.Document.Responses;
using NOTE.Solutions.Entities.Entities.Order;

namespace NOTE.Solutions.BLL.Services;

public class ReceiptService : IReceiptService
{
    private string _cachedKey;
    private int _userId;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string[] _includes =
    {
        $"{nameof(Order.OrderDetails)}",
        $"{nameof(Order.Customer)}",
        $"{nameof(Order.OrderDetails)}.{nameof(OrderLine.ProductUnit)}"

    };
    public ReceiptService (IHttpContextAccessor httpContextAccessor,ICacheService cacheService,IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
        _httpContextAccessor = httpContextAccessor;

        _userId = _httpContextAccessor.HttpContext!.User.GetUserId();
        _cachedKey = $"receipts_user_{_userId}";
    }

    public async Task<Result<string>> GetNextNumberAsync(int branchId,CancellationToken cancellationToken = default)
    {
        var lastReceiptNumber = await _unitOfWork.Orders.MaxAsync(x => x.BranchId == branchId, x => x.Id);

        return Result.Success((++lastReceiptNumber).ToString());
    }

    public async Task<Result<OrderResponse>> CreateAsync(int branchId, OrderRequest documentRequest, CancellationToken cancellationToken = default)
    {
        #region validation
        if (!_unitOfWork.Branches.IsExist(x => x.Id == branchId))
            return Result.Failure<OrderResponse>(BranchErrors.NotFound);

        if (!_unitOfWork.Branches.IsExist(x => (x.Id == branchId) && (x.PointOfSales.Any(x => x.Id == documentRequest.PosId))))
            return Result.Failure<OrderResponse>(POSErrors.NotFound);

        if(!_unitOfWork.Companies.IsExist(x=>x.ActiveCodeCompanies.Any(X=>X.ActiveCodeId == documentRequest.ActiveCodeId)))
            return Result.Failure<OrderResponse>(ActiveCodeErrors.NotFound);

        foreach (var documentDetails in documentRequest.OrderLines)
        {
            if (!_unitOfWork.ProductUnits.IsExist(x => x.Id == documentDetails.ProductUnitId))
                return Result.Failure<OrderResponse>(ProductUnitErrors.NotFound);
        }
        #endregion

        var document = documentRequest.Adapt<Order>();
        document.BranchId = branchId;
        
        await _unitOfWork.Orders.AddAsync(document, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);


        await _cacheService.RemoveAsync(_cachedKey);

        return Result.Success(document.Adapt<OrderResponse>());

    }
    
    public Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public async Task<Result<IEnumerable<OrderResponse>>> GetAllAsync(int branchId,CancellationToken cancellationToken = default)
    {
        if (!_unitOfWork.Branches.IsExist(x => x.Id == branchId))
            return Result.Failure<IEnumerable<OrderResponse>>(BranchErrors.NotFound);

        var cachedReceipts = await _cacheService.GetAsync<IEnumerable<OrderResponse>>(_cachedKey, cancellationToken);

        if(cachedReceipts is not null)
            return Result.Success(cachedReceipts);

        var documents = await _unitOfWork.Orders.FindAllAsync(x=>x.BranchId == branchId,includes: _includes, cancellationToken: cancellationToken);

        var receipts = documents.Adapt<IEnumerable<OrderResponse>>();

        await _cacheService.SetAsync(_cachedKey, receipts, TimeSpan.FromDays(10));

        return Result.Success(receipts);
    }
    public Task<Result<OrderResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public Task<Result> UpdateAsync(OrderRequest documentRequest, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    #region Helper
    //private async Task<Result<string>> GetAccessTokenAsync(int posId)
    //{
    //    int customerId = _httpContextAccessor.HttpContext!.User.GetUserId();

    //    var cacheKey = $"cached-access-token/posId/{posId}";

    //    var accessToken = await _cacheService.GetAsync<string>(cacheKey);

    //    if (accessToken is not null)
    //        return Result.Success(accessToken);

    //    var pos = await _unitOfWork.POSs.FindAsync(x => x.Id == posId);

    //    if (pos is null)
    //        return Result.Failure<string>(POSErrors.NotFound);

    //    var authPosRequest = new AuthPOSRequest()
    //    {
    //        ClientId = pos!.ClientId,
    //        ClientSecret = pos.ClientSecret,
    //        POSSerial = pos.POSSerial,
    //    };

    //    var result = await _etaManager.EtaAuthService.AuthenticatePOSAsync(authPosRequest);

    //    if (!result.IsSuccess)
    //        return Result.Failure<string>(new Error("", JsonConvert.SerializeObject(result.Error), result.StatusCode));

    //    await _cacheService.SetAsync<string>(cacheKey, result.Data!.AccessToken, TimeSpan.FromSeconds(result.Data.ExpiresIn));

    //    return Result.Success(result.Data.AccessToken);
    //}
    //private async Task<Result> SubmitAsync(int posId, int activeCodeId, Document document)
    //{
    //    var accessTokenResult = await GetAccessTokenAsync(posId);

    //    if (!accessTokenResult.IsSuccess)
    //    {

    //    }

    //    var pos = await _unitOfWork.POSs.FindAsync(x => x.Id == posId, new string[]
    //    {
    //        nameof(POS.Branch),
    //        $"{nameof(POS.Branch)}.{nameof(POS.Branch.Company)}",
    //        $"{nameof(POS.Branch)}.{nameof(POS.Branch.Company)}.{nameof(Company.CompanyActiveCodes)}",
    //        $"{nameof(POS.Branch)}.{nameof(POS.Branch.Company)}.{nameof(Company.CompanyActiveCodes)}",
    //        $"{nameof(POS.Branch)}.{nameof(POS.Branch.City)}",
    //        $"{nameof(POS.Branch)}.{nameof(POS.Branch.City)}.{nameof(City.Governorate)}",
    //        $"{nameof(POS.Branch)}.{nameof(POS.Branch.City)}.{nameof(City.Governorate)}.{nameof(Governorate.Country)}",
    //    });

    //    if (pos is null)
    //        return Result.Failure<SubmitReceiptsResponse>(POSErrors.NotFound);

    //    var documents = new SubmitReceiptsRequest()
    //    {
    //        Receipts = [new SubmitReceiptRequest(){
    //            Header = new ETA.Consume.Contracts.Header.Requests.HeaderRequest()
    //            {
    //                DateTimeIssued = document.Header.DateTime,
    //                Currency = "EGP",
    //                ExchangeRate = 0,
    //                ReceiptNumber = document.Header.DocumentNumber,

    //            },
    //            DocumentType = new ETA.Consume.Contracts.DocumentType.Requests.DocumentTypeRequest()
    //            {
    //                ReceiptType = document.DocumentType.Type,
    //                TypeVersion = document.DocumentType.Version
    //            },
    //            Seller = new ETA.Consume.Contracts.Seller.Requests.SellerRequest()
    //            {
    //                CompanyTradeName = pos.Branch.Company.Name,
    //                DeviceSerialNumber = pos.POSSerial,
    //                ActivityCode = pos.Branch.Company.CompanyActiveCodes.First(x=>x.ActiveCodeId == activeCodeId).ActiveCode.Code,
    //                BranchCode  = pos.Branch.Code,
    //                RIN = pos.Branch.Company.RIN,
    //                BranchAddress = new ETA.Consume.Contracts.BranchAddress.Requests.BranchAddressRequest()
    //                {
    //                    Country = pos.Branch.City.Governorate.Country.Code,
    //                    Governate = pos.Branch.City.Governorate.Name,
    //                    RegionCity = pos.Branch.City.Code,
    //                    BuildingNumber = pos.Branch.BuildingNumber,
    //                    Street = pos.Branch.Street,
    //                },
    //            },
    //            Buyer = new ETA.Consume.Contracts.Buyer.Requests.BuyerRequest()
    //            {
    //                Id = document.Buyer.SSN,
    //                Name = document.Buyer.Name,
    //                //MobileNumber = document.Buyer,
    //                Type = document.Buyer.Type.ToString(),
    //            },
    //            ItemData = document.DocumentDetails.Select(d => new ETA.Consume.Contracts.Item.Requests.ItemRequest()
    //            {
    //                InternalCode = d.ProductUnit.InternalCode,
    //                Description = d.ProductUnit.Description,
    //                ItemType = d.ProductUnit.GlobalCodeType.ToString(),
    //                Quantity = d.Quantity,
    //                UnitType = d.ProductUnit.Unit!.Code,
    //                UnitPrice = d.UnitPrice,
    //                ItemCode = d.ProductUnit.GlobalCode,
    //                TotalSale = d.Quantity * d.UnitPrice,
    //                NetSale = d.Quantity * d.UnitPrice,
    //                Total = d.Quantity * d.UnitPrice,
    //            }).ToList(),
    //            TotalSales = document.DocumentDetails.Sum(d => d.Quantity * d.UnitPrice),
    //            NetAmount = document.DocumentDetails.Sum(d => d.Quantity * d.UnitPrice),
    //            TotalAmount = document.DocumentDetails.Sum(d => d.Quantity * d.UnitPrice),
    //            PaymentMethod = document.PaymentMethod.ToString(),
    //        }]
    //    };

    //    var submitResult = await _etaManager.ReceiptService.SubmitReceiptsAsync(accessTokenResult.Value, documents);

    //    if (!submitResult.IsSuccess)
    //        return Result.Failure<SubmitReceiptsResponse>(new Error(submitResult.StatusCode.ToString(), submitResult.Error.Error, submitResult.StatusCode));

    //    BackgroundJob.Schedule(() =>
    //        CheckStatusAsync(submitResult.Data!.SubmissionId, accessTokenResult.Value), TimeSpan.FromSeconds(10)
    //    );

    //    return Result.Success(submitResult.Data!);
    //}
    //private async Task CheckStatusAsync(string subuuid, string token)
    //{
    //    var result = await _etaManager.ReceiptService.GetReceiptSubmission(subuuid, token);

    //    if (!result.IsSuccess)
    //        return;

    //    switch (result.Data?.Status)
    //    {

    //        case "Valid":
    //        case "Invalid":
    //            _logger.LogWarning(result.Data.Status);
    //            break;

    //        case "InProgress":
    //        default:
    //            _logger.LogWarning(result.Data!.Status);
    //            BackgroundJob.Schedule(() => CheckStatusAsync(subuuid, token), TimeSpan.FromSeconds(10));
    //            break;
    //    }
    //} 
    #endregion
}
