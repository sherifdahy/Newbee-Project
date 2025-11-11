namespace ETA.Consume.Interfaces;

public interface IEtaManager
{
    IEtaAuthService EtaAuthService { get; }
    IReceiptService ReceiptService { get; }
}
