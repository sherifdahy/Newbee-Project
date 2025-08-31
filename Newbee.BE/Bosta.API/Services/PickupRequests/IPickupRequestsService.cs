using Api.Bosta.DTOs.PickupRequests.Requests;
using Api.Bosta.DTOs.PickupRequests.Responses;

namespace Bosta.API.Services.PickupRequests
{
    public interface IPickupRequestsService
    {
        Task<ApiResponse<PickupRequestCreateDataDTO>> CreateAsync(PickupRequestCreateRequestDTO requestDTO);
    }
}
