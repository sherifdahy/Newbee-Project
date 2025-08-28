using Api.Bosta.DTOs.PickupLocations.Responses;
using Api.Bosta.DTOs.Shared;
using Bosta.API.Services.ApiCall;
using System.Net.Http.Json;

namespace Bosta.API.Services.PickupLocations
{
    public class PickupLocationsService : IPickupLocationsService
    {
        private readonly IApiCall _apiCall;

        public PickupLocationsService(IApiCall apiCall)
        {
            _apiCall = apiCall;
        }

        public async Task<ApiResponseDTO<PickupLocationAddDataDTO>> AddPickupLocationsAsync(PickupLocationAddRequestDTO dto)
        {
            if(dto == null) 
                throw new ArgumentNullException(nameof(dto));

            return await _apiCall.PostAsync<PickupLocationAddRequestDTO,ApiResponseDTO<PickupLocationAddDataDTO>>("pickup-locations", dto);
        }

        public async Task<ApiResponseDTO<object>> DefaultLocation(string id)
        {
            return await _apiCall.PutAsync<object, ApiResponseDTO<object>>($"pickup-locations/{id}/default",null);
        }

        public async Task<ApiResponseDTO<object>> Delete(string id)
        {
            return await _apiCall.DeleteAsync<ApiResponseDTO<object>>($"pickup-locations/{id}");
        }

        public async Task<ApiResponseDTO<PickupLocationListItemDTO>> GetAllAsync()
        {
            return await _apiCall.GetAsync<ApiResponseDTO<PickupLocationListItemDTO>>("pickup-locations");
        }

        public async Task<ApiResponseDTO<PickupLocationListItemDTO>> GetByIdAsync(string id)
        {
            return await _apiCall.GetAsync<ApiResponseDTO<PickupLocationListItemDTO>>($"pickup-locations/{id}");
        }
    }

}
