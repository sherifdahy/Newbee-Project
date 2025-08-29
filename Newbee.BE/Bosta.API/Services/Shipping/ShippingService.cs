using Api.Bosta.DTOs.Shipping.Request;
using Api.Bosta.DTOs.Shipping.Response;
using Bosta.API.DTOs.Shared;
using Bosta.API.Services.ApiCall;
using Bosta.API.Services.Shipping;
using System.Net.Http.Json;

namespace Api.Bosta.Services.Shipping
{
    public class ShippingService : IShippingService
    {
        private readonly IApiCall _apiCall;
        private readonly IDictionary<string, string> _headers;
        public ShippingService(IApiCall apiCall)
        {
            this._apiCall = apiCall;
            _headers = new Dictionary<string, string>();
            
        }
        public async Task<ApiResponseDTO<CreateShipmentDataDTO>> CreateAsync(CreateShipmentRequestDTO data,string apiKey)
        {
            if (apiKey is null)
                throw new ArgumentNullException(nameof(apiKey));

            _headers["Authorization"] = apiKey;

            return await this._apiCall.PostAsync<CreateShipmentRequestDTO, ApiResponseDTO<CreateShipmentDataDTO>>("deliveries", data,_headers);
        }
        public async Task<ApiResponseDTO<ShipmentDataDTO>> GetByTrackingNumberAsync(string trackingNumber,string apiKey)
        {
            if (apiKey is null)
                throw new ArgumentNullException(nameof(apiKey));

            _headers["Authorization"] = apiKey;

            return await _apiCall.GetAsync<ApiResponseDTO<ShipmentDataDTO>>($"deliveries/business/{trackingNumber}",_headers);
        }
        public async Task<ApiResponseDTO<TerminateDeliveryDataDTO>> TerminateAsync(string trackingNumber, string apiKey)
        {
            if (apiKey is null)
                throw new ArgumentNullException(nameof(apiKey));

            _headers["Authorization"] = apiKey;

            return await this._apiCall.DeleteAsync<ApiResponseDTO<TerminateDeliveryDataDTO>>($"deliveries/business/{trackingNumber}/terminate",_headers);
        }

    }
}
