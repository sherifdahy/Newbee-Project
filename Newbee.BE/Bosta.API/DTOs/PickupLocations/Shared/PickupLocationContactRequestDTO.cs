using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.PickupLocations.Shared
{
    public class PickupLocationContactRequestDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string IsDefault { get; set; } // لاحظ إنها string مش bool بناءً على الـ curl
    }

}
