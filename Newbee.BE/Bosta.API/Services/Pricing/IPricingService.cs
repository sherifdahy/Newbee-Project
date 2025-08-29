using Api.Bosta.DTOs.Price;
using Bosta.API.DTOs.Price;
using Bosta.API.DTOs.Shared;

namespace Bosta.API.Services.Price
{
    public interface IPricingService
    {
        Task<ApiResponseDTO<PricingDataDTO>> PricingCalculator(PricingRequestDTO pricingRequestDTO,string apiKey);
    }
}
