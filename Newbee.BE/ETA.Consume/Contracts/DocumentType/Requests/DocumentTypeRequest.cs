namespace ETA.Consume.Contracts.DocumentType.Requests;

public class DocumentTypeRequest
{
    public string ReceiptType { get; set; } = string.Empty;
    public string TypeVersion { get; set; } = string.Empty;
}
