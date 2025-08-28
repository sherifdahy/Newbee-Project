using Api.Bosta.DTOs.Lookup;
using Api.Bosta.DTOs.Shared;

namespace Bosta.API.Services.Lookup
{
    public interface ILookupService
    {
        //Task<ApiResponseDTO<GovernorateDataDto>> GetAllCitiesAsync(string countryId = "60e4482c7cb7d4bc4849c4d5");
        //Task<ApiResponseDTO<CityDetailsDTO>> GetCityInfoAsync();
        //Task<ApiResponseDTO<List<DistrictDTO>>> GetAllDistrictsAsync();
        //Task<ApiResponseDTO<List<ZoneDTO>>> GetAllZonesAsync(string cityId);
        Task<ApiResponseDTO<List<ZoneDistrictDto>>> GetDistrictInfoAsync(string cityId);
    }
}