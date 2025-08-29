using Api.Bosta.DTOs.Lookup;
using Bosta.API.DTOs.Shared;
using Bosta.API.Services.ApiCall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Bosta.API.Services.Lookup
{
    public class LookupService : ILookupService
    {
        private readonly IApiCall _apiCall;
        public LookupService(IApiCall apiCall)
        {
            this._apiCall = apiCall;

        }
        //public Task<ApiResponseDTO<List<DistrictDTO>>> GetAllDistrictsAsync()
        //{
        //    throw new NotImplementedException();
        //}
        //public async Task<ApiResponseDTO<CityListResponse>> GetAllCitiesAsync(string countryId = "60e4482c7cb7d4bc4849c4d5")
        //{
        //    var response = await _apiCall.GetAsync($"cities?countryId={countryId}");

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new Exception($"فشل في إنشاء الشحنة. كود الخطأ: {response.StatusCode}");
        //    }
        //    var result = await response.Content.ReadAsStringAsync();

        //    var respJson = await response.Content.ReadFromJsonAsync<ApiResponseDTO<CityListResponse>>();

        //    if (respJson == null)
        //        throw new Exception("فشل في قراءة استجابة الخادم.");

        //    return respJson;
        //}
        //public async Task<ApiResponseDTO<List<ZoneDTO>>> GetAllZonesAsync(string cityId)
        //{
        //    var response = await _apiCall.GetAsync($"cities/{cityId}/zones");

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new Exception($"فشل في إنشاء الشحنة. كود الخطأ: {response.StatusCode}");
        //    }

        //    var result = await response.Content.ReadAsStringAsync();

        //    var respJson = await response.Content.ReadFromJsonAsync<ApiResponseDTO<List<ZoneDTO>>>();

        //    if (respJson == null)
        //        throw new Exception("فشل في قراءة استجابة الخادم.");

        //    return respJson;
        //}
        public async Task<ApiResponseDTO<List<ZoneDistrictDto>>> GetDistrictInfoAsync(string cityId)
        {
            return await _apiCall.GetAsync<ApiResponseDTO<List<ZoneDistrictDto>>>($"cities/{cityId}/districts");
        }
        //public Task<ApiResponseDTO<CityDetailsDTO>> GetCityInfoAsync()
        //{
        //    throw new NotImplementedException();
        //}


    }
}