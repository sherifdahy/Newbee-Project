using Api.Bosta.DTOs.Shipping.Request;
using Api.Bosta.DTOs.Shipping.Response;
using Bosta.API.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bosta.API.Services.Shipping
{
    public interface IShippingService
    {
        Task<ApiResponseDTO<CreateShipmentDataDTO>> CreateAsync(CreateShipmentRequestDTO data, string apiKey);
        Task<ApiResponseDTO<TerminateDeliveryDataDTO>> TerminateAsync(string trackingNumber, string apiKey);
        Task<ApiResponseDTO<ShipmentDataDTO>> GetByTrackingNumberAsync(string trackingNumber, string apiKey);
    }
}
