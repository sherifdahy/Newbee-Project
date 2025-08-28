using Api.Bosta.DTOs.Price;
using Api.Bosta.DTOs.Shared;
using Bosta.API.Services.ApiCall;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Bosta.API.Services.Price
{
    public class PricingService : IPricingService
    {
        private readonly IApiCall _apiCall;
        public PricingService(IApiCall apiCall)
        {
            _apiCall = apiCall;
        }
        public async Task<ApiResponseDTO<PricingDataDTO>> PricingCalculator(decimal cashOnDeliveryAmount ,string dropOffCity ,string pickupCity ,string size = "Normal" ,string type = "SEND")
        {
            return await _apiCall.GetAsync<ApiResponseDTO<PricingDataDTO>>($"pricing/shipment/calculator?cod{cashOnDeliveryAmount}&dropOffCity={dropOffCity}&pickupCity={pickupCity}&size={size}");
        }
    }
}
