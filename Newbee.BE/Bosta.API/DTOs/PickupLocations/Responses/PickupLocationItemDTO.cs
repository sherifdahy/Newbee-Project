using Api.Bosta.DTOs.PickupLocations.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.PickupLocations.Responses
{
    public class PickupLocationItemDTO
    {
        public string LocationName { get; set; }
        public PickupLocationContactPersonDTO ContactPerson { get; set; }
        public PickupLocationAddressRequestDTO Address { get; set; }
        [JsonPropertyName("_id")]
        public string Id { get; set; } // يمثل _id
        public List<object> Contacts { get; set; }
        public bool IsDefault { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}
