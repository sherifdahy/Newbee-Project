using Api.Bosta.DTOs.Price;
using Api.Bosta.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bosta.API.Services.Price
{
    public interface IPricingService
    {
        Task<ApiResponseDTO<PricingDataDTO>> PricingCalculator(decimal cashOnDeliveryAmount , string dropOffCity, string pickupCity, string size = "Normal",string type = "SEND");
    }
}
