using Api.Bosta.DTOs.PickupLocations.Responses;
using Bosta.API.Services.ApiCall;
using System.Net.Http.Json;

namespace Bosta.API.Services.PickupLocations
{
    public class PickupLocationsService : IPickupLocationsService
    {
        private readonly IApiCall _apiCall;
        private readonly IDictionary<string, string> _headers;

        public PickupLocationsService(IApiCall apiCall)
        {
            _apiCall = apiCall;
            _headers = new Dictionary<string, string>();
        }

        public async Task<ApiResponse<PickupLocationAddDataDTO>> AddPickupLocationsAsync(PickupLocationAddRequestDTO dto,string apiKey)
        {
            _headers["Authorization"] = apiKey;

            return await _apiCall.PostAsync<PickupLocationAddRequestDTO, ApiResponse<PickupLocationAddDataDTO>>("pickup-locations", dto, _headers);
        }

        public async Task<ApiResponse<object>> DefaultLocation(string id, string apiKey)
        {
            return await _apiCall.PutAsync<object, ApiResponse<object>>($"pickup-locations/{id}/default",null, _headers);
        }

        public async Task<ApiResponse<object>> Delete(string id, string apiKey)
        {
            _headers["Authorization"] = apiKey;

            return await _apiCall.DeleteAsync<ApiResponse<object>>($"pickup-locations/{id}", _headers);
        }

        public async Task<ApiResponse<PickupLocationListItemDTO>> GetAllAsync(string apiKey)
        {
            _headers["Authorization"] = apiKey;

            return await _apiCall.GetAsync<ApiResponse<PickupLocationListItemDTO>>("pickup-locations", _headers);
        }

        public async Task<ApiResponse<PickupLocationListItemDTO>> GetByIdAsync(string id, string apiKey)
        {
            _headers["Authorization"] = apiKey;

            return await _apiCall.GetAsync<ApiResponse<PickupLocationListItemDTO>>($"pickup-locations/{id}", _headers);
        }
    }

}
