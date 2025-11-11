using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETA.Consume.Contracts.Receipt.Responses;

public class SubmitReceiptsResponse
{
    public string SubmissionId { get; set; } = string.Empty;
    public List<AcceptedDocumentResponse> AcceptedDocuments { get; set; } = [];
    public List<RejectedDocumentResponse> RejectedDocuments { get; set; } = [];
}





