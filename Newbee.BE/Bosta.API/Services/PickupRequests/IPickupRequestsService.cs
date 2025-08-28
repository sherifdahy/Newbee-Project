using Api.Bosta.DTOs.PickupRequests.Requests;
using Api.Bosta.DTOs.PickupRequests.Responses;
using Api.Bosta.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bosta.API.Services.PickupRequests
{
    public interface IPickupRequestsService
    {
        Task<ApiResponseDTO<PickupRequestCreateDataDTO>> CreateAsync(PickupRequestCreateRequestDTO requestDTO);
    }
}
