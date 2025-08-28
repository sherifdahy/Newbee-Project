using Api.Bosta.DTOs.Shared;
using Api.Bosta.DTOs.Shipping.Request;
using Api.Bosta.DTOs.Shipping.Response;
using Bosta.API.Services.ApiCall;
using Bosta.API.Services.Shipping;
using System.Net.Http.Json;

namespace Api.Bosta.Services.Shipping
{
    public class ShippingService : IShippingService
    {
        private readonly IApiCall _apiCall;
        public ShippingService(IApiCall apiCall)
        {
            this._apiCall = apiCall;
            
        }
        public async Task<ApiResponseDTO<CreateShipmentDataDTO>> CreateAsync(CreateShipmentRequestDTO data)
        {
            return await this._apiCall.PostAsync<CreateShipmentRequestDTO, ApiResponseDTO<CreateShipmentDataDTO>>("deliveries", data);
        }
        public async Task<ApiResponseDTO<ShipmentDataDTO>> GetByTrackingNumberAsync(string trackingNumber)
        {
            return await _apiCall.GetAsync<ApiResponseDTO<ShipmentDataDTO>>($"deliveries/business/{trackingNumber}");
        }
        public async Task<ApiResponseDTO<TerminateDeliveryDataDTO>> TerminateAsync(string trackingNumber)
        {
            return await this._apiCall.DeleteAsync<ApiResponseDTO<TerminateDeliveryDataDTO>>($"deliveries/business/{trackingNumber}/terminate");
        }

    }
}
