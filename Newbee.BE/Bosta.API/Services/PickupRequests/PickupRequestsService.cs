using Api.Bosta.DTOs.PickupRequests.Requests;
using Api.Bosta.DTOs.PickupRequests.Responses;
using Bosta.API.DTOs.Shared;
using Bosta.API.Services.ApiCall;

namespace Bosta.API.Services.PickupRequests
{
    public class PickupRequestsService : IPickupRequestsService
    {
        private readonly IApiCall _apiCall;

        public PickupRequestsService(IApiCall apiCall)
        {
            _apiCall = apiCall;
        }

        public async Task<ApiResponseDTO<PickupRequestCreateDataDTO>> CreateAsync(PickupRequestCreateRequestDTO requestDTO)
        {
            return await _apiCall.PostAsync<PickupRequestCreateRequestDTO,ApiResponseDTO<PickupRequestCreateDataDTO>>("pickups", requestDTO);
        }

        public async Task<ApiResponseDTO<List<PickupRequestCreateDataDTO>>> GetAllAsync()
        {
            return await _apiCall.GetAsync<ApiResponseDTO<List<PickupRequestCreateDataDTO>>>("pickups");
        }

        public async Task<ApiResponseDTO<PickupRequestCreateDataDTO>> GetByIdAsync(string id)
        {
            return await _apiCall.GetAsync<ApiResponseDTO<PickupRequestCreateDataDTO>>($"pickups/{id}");
        }

        public async Task<ApiResponseDTO<object>> Delete(string id)
        {
            return await _apiCall.DeleteAsync<ApiResponseDTO<object>>($"pickups/{id}");
        }
    }

}
