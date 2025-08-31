using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Lookup
{
    public class GovernorateDataDto
    {
        public List<GovernorateDto> List { get; set; }
    }

    public class GovernorateDto
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        public string Name { get; set; }

        public string NameAr { get; set; }

        public string Code { get; set; }

        public string Alias { get; set; }

        public HubDto Hub { get; set; }

        public int Sector { get; set; }

        public bool PickupAvailability { get; set; }

        public bool DropOffAvailability { get; set; }

        public bool ShowAsDropOff { get; set; }

        public bool ShowAsPickup { get; set; }
    }

    public class HubDto
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        public string Name { get; set; }
    }

    

    public class ZoneDistrictDto
    {
        public string ZoneId { get; set; }
        public string ZoneName { get; set; }
        public string ZoneOtherName { get; set; }
        public string DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string DistrictOtherName { get; set; }
        public bool PickupAvailability { get; set; }
        public bool DropOffAvailability { get; set; }
    }

    public class ZoneDto
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        public string Name { get; set; }

        public string NameAr { get; set; }

        public bool PickupAvailability { get; set; }

        public bool DropOffAvailability { get; set; }
    }
}
