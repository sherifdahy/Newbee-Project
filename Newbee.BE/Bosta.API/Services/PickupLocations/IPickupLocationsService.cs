using Api.Bosta.DTOs.PickupLocations.Responses;
using Bosta.API.DTOs.Shared;

namespace Bosta.API.Services.PickupLocations
{
    public interface IPickupLocationsService
    {
        Task<ApiResponseDTO<PickupLocationAddDataDTO>> AddPickupLocationsAsync(PickupLocationAddRequestDTO pickupLocationAddRequestDTO);
        Task<ApiResponseDTO<PickupLocationListItemDTO>> GetByIdAsync(string id);
        Task<ApiResponseDTO<object>> Delete(string id);
        Task<ApiResponseDTO<object>> DefaultLocation(string id);
        Task<ApiResponseDTO<PickupLocationListItemDTO>> GetAllAsync();
    }
}
