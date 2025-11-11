using ETA.Consume.Abstractions;
namespace ETA.Consume.Interfaces;

public interface IReceiptService
{
    Task<ApiResult<SubmitReceiptsResponse>> SubmitReceiptsAsync(string accessToken, SubmitReceiptsRequest Documents);
    Task<ApiResult<ReceiptSubmissionResponse>> GetReceiptSubmission(string submissionUuid, string accessToken);
}
