using Api.Bosta.DTOs.Shared;
using Api.Bosta.DTOs.Shipping.Request;
using Api.Bosta.DTOs.Shipping.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bosta.API.Services.Shipping
{
    public interface IShippingService
    {
        Task<ApiResponseDTO<CreateShipmentDataDTO>> CreateAsync(CreateShipmentRequestDTO data);
        Task<ApiResponseDTO<TerminateDeliveryDataDTO>> TerminateAsync(string trackingNumber);
        Task<ApiResponseDTO<ShipmentDataDTO>> GetByTrackingNumberAsync(string trackingNumber);
    }
}
