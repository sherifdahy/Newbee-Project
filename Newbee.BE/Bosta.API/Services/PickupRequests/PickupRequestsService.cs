using Api.Bosta.DTOs.PickupRequests.Requests;
using Api.Bosta.DTOs.PickupRequests.Responses;
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

        public async Task<ApiResponse<PickupRequestCreateDataDTO>> CreateAsync(PickupRequestCreateRequestDTO requestDTO)
        {
            return await _apiCall.PostAsync<PickupRequestCreateRequestDTO, ApiResponse<PickupRequestCreateDataDTO>>("pickups", requestDTO);
        }

        public async Task<ApiResponse<List<PickupRequestCreateDataDTO>>> GetAllAsync()
        {
            return await _apiCall.GetAsync<ApiResponse<List<PickupRequestCreateDataDTO>>>("pickups");
        }

        public async Task<ApiResponse<PickupRequestCreateDataDTO>> GetByIdAsync(string id)
        {
            return await _apiCall.GetAsync<ApiResponse<PickupRequestCreateDataDTO>>($"pickups/{id}");
        }

        public async Task<ApiResponse<object>> Delete(string id)
        {
            return await _apiCall.DeleteAsync<ApiResponse<object>>($"pickups/{id}");
        }
    }

}
