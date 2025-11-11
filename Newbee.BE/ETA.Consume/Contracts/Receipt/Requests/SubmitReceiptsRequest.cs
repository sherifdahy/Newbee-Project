using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETA.Consume.Contracts.Receipt.Requests;

public class SubmitReceiptsRequest
{
    public List<SubmitReceiptRequest> Receipts { get; set; } = [];
}
