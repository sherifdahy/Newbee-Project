using Api.Bosta.DTOs.Shipping.Request;
using Api.Bosta.DTOs.Shipping.Response;
using Bosta.API;
using Bosta.API.Services.ApiCall;
using Bosta.API.Services.Shipping;

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
        public async Task<ApiResponse<CreateShipmentDataDTO>> CreateAsync(CreateShipmentRequestDTO data,string apiKey)
        {
            _headers["Authorization"] = apiKey;

            return await this._apiCall.PostAsync<CreateShipmentRequestDTO, ApiResponse<CreateShipmentDataDTO>>("deliveries", data, _headers);
        }
        public async Task<ApiResponse<ShipmentDataDTO>> GetByTrackingNumberAsync(string trackingNumber,string apiKey)
        {
            _headers["Authorization"] = apiKey;

            return await _apiCall.GetAsync<ApiResponse<ShipmentDataDTO>>($"deliveries/business/{trackingNumber}",_headers);
        }
        public async Task<ApiResponse<TerminateDeliveryDataDTO>> TerminateAsync(string trackingNumber, string apiKey)
        {
            _headers["Authorization"] = apiKey;

            return await this._apiCall.DeleteAsync<ApiResponse<TerminateDeliveryDataDTO>>($"deliveries/business/{trackingNumber}/terminate",_headers);
        }

    }
}
