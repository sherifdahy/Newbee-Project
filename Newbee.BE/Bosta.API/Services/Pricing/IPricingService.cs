using Api.Bosta.DTOs.Price;
using Bosta.API.DTOs.Price;

namespace Bosta.API.Services.Price
{
    public interface IPricingService
    {
        Task<ApiResponse<PricingDataDTO>> PricingCalculator(PricingRequestDTO pricingRequestDTO,string apiKey);
    }
}
