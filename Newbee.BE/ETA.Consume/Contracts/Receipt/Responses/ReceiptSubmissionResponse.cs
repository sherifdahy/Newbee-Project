
namespace ETA.Consume.Contracts.Receipt.Responses;

public class ReceiptSubmissionResponse
{
    public string SubmissionUuid { get; set; }
    public string SellerId { get; set; }
    public DateTime DateTimeReceived { get; set; }
    public string SubmitterName { get; set; }
    public string DeviceSerialNumber { get; set; }
    public string SubmissionChannel { get; set; }
    public int ReceiptsCount { get; set; }
    public int InvalidReceiptsCount { get; set; }
    public string Status { get; set; }
    public List<ReceiptResponse> Receipts { get; set; }
    public MetadataResponse Metadata { get; set; }
}

public class ReceiptResponse
{
    public string Uuid { get; set; }

    public string ReceiptNumber { get; set; }

    public DateTime DateTimeIssued { get; set; }

    public DateTime DateTimeReceived { get; set; }

    public string DocumentType { get; set; }

    public string DocumentTypeNamePrimaryLang { get; set; }

    public string DocumentTypeNameSecondaryLang { get; set; }

    public string Currency { get; set; }

    public string CurrencyNamePrimaryLang { get; set; }

    public string CurrencyNameSecondaryLang { get; set; }

    public string DocumentTypeVersion { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal ExchangeRate { get; set; }

    public string SellerID { get; set; }

    public string SellerName { get; set; }

    public string BuyerId { get; set; }

    public string BuyerName { get; set; }

    public string BuyerType { get; set; }

    public string Status { get; set; }
    public string LongId { get; set; }
    public string DeviceSerialNumber { get; set; }

    public bool HasReturnReceipts { get; set; }
}

public class MetadataResponse
{
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int CurrentPageNo { get; set; }
}

