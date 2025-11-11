namespace ETA.Consume.Manager;

public class EtaManager : IEtaManager
{
    private readonly Lazy<IEtaAuthService> _etaAuthService;
    private readonly Lazy<IReceiptService> _receiptService;

    private readonly BaseApiCallService _identityApiCall;
    private readonly BaseApiCallService _apicall;
    private readonly IUUIDService _uuidService;
    public EtaManager(IOptions<ETAOptions> options)
    {
        _identityApiCall = new BaseApiCallService(new HttpClient() { BaseAddress = new Uri(options.Value.IdSrvBaseUrl) });
        _apicall = new BaseApiCallService(new HttpClient() { BaseAddress = new Uri(options.Value.ApiBaseUrl) });
        _uuidService = new UUIDService();

        _etaAuthService = new Lazy<IEtaAuthService>(() => new EtaAuthService(_identityApiCall));
        _receiptService = new Lazy<IReceiptService>(() => new ReceiptService(_apicall, _uuidService));
    }
    public IEtaAuthService EtaAuthService => _etaAuthService.Value;
    public IReceiptService ReceiptService => _receiptService.Value;
}
