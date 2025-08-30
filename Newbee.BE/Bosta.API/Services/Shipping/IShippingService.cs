using Api.Bosta.DTOs.Shipping.Request;
using Api.Bosta.DTOs.Shipping.Response;

namespace Bosta.API.Services.Shipping
{
    public interface IShippingService
    {
        Task<ApiResponse<CreateShipmentDataDTO>> CreateAsync(CreateShipmentRequestDTO data, string apiKey);
        Task<ApiResponse<TerminateDeliveryDataDTO>> TerminateAsync(string trackingNumber, string apiKey);
        Task<ApiResponse<ShipmentDataDTO>> GetByTrackingNumberAsync(string trackingNumber, string apiKey);
    }
}
