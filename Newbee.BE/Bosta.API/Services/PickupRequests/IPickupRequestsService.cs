using Api.Bosta.DTOs.PickupRequests.Requests;
using Api.Bosta.DTOs.PickupRequests.Responses;
using Bosta.API.DTOs.Shared;

namespace Bosta.API.Services.PickupRequests
{
    public interface IPickupRequestsService
    {
        Task<ApiResponseDTO<PickupRequestCreateDataDTO>> CreateAsync(PickupRequestCreateRequestDTO requestDTO);
    }
}
