using Api.Bosta.DTOs.PickupLocations.Responses;

namespace Bosta.API.Services.PickupLocations
{
    public interface IPickupLocationsService
    {
        Task<ApiResponse<PickupLocationAddDataDTO>> AddPickupLocationsAsync(PickupLocationAddRequestDTO pickupLocationAddRequestDTO, string apiKey);
        Task<ApiResponse<PickupLocationListItemDTO>> GetByIdAsync(string id, string apiKey);
        Task<ApiResponse<object>> Delete(string id, string apiKey);
        Task<ApiResponse<object>> DefaultLocation(string id, string apiKey);
        Task<ApiResponse<PickupLocationListItemDTO>> GetAllAsync( string apiKey);
    }
}
