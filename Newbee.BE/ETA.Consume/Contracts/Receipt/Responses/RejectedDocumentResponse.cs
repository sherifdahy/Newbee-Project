using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETA.Consume.Contracts.Receipt.Responses;

public class RejectedDocumentResponse
{
    public string UUID { get; set; } = string.Empty;
    public string ReceiptNumber { get; set; } = string.Empty;
    public Error.Responses.Error Error { get; set; } = default!;
}
