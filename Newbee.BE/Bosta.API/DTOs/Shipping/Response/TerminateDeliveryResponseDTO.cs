using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Response
{
    public class TerminateDeliveryResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public TerminateDeliveryDataDTO Data { get; set; }
    }
    public class TerminateDeliveryDataDTO
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
    }
}
