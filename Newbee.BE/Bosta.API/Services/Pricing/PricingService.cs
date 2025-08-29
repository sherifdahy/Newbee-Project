using Api.Bosta.DTOs.Price;
using Bosta.API.DTOs.Price;
using Bosta.API.DTOs.Shared;
using Bosta.API.Services.ApiCall;

namespace Bosta.API.Services.Price
{
    public class PricingService : IPricingService
    {
        private readonly IApiCall _apiCall;
        private readonly IDictionary<string, string> _headers;
        public PricingService(IApiCall apiCall)
        {
            _apiCall = apiCall;
            _headers = new Dictionary<string, string>();
        }
        public async Task<ApiResponseDTO<PricingDataDTO>> PricingCalculator(PricingRequestDTO pricingRequestDTO,string apiKey)
        {
            if(apiKey is null)
                throw new ArgumentNullException(nameof(apiKey));

            _headers["Authorization"] = apiKey;

            return await _apiCall.GetAsync<ApiResponseDTO<PricingDataDTO>>($"pricing/shipment/calculator?cod{pricingRequestDTO.Cod}&dropOffCity={pricingRequestDTO.DropOffCity}&pickupCity={pricingRequestDTO.PickupCity}&size={pricingRequestDTO.Size}",_headers);
        }
    }
}
