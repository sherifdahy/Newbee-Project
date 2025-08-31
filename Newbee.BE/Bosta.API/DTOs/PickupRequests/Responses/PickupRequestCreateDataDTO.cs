using Api.Bosta.DTOs.PickupLocations.Shared;
using Api.Bosta.DTOs.PickupRequests.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.PickupRequests.Responses
{
    public class PickupRequestCreateDataDTO
    {
        public string Type { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string ScheduledTimeSlot { get; set; }
        public string State { get; set; }
        public List<object> Tickets { get; set; }
        public string PackageType { get; set; }
        public List<object> ReschduledPickupsList { get; set; }
        public string Puid { get; set; }
        public BusinessDTO Business { get; set; }
        public PickupLocationContactPersonDTO ContactPerson { get; set; }
        public string Notes { get; set; }
        public string BusinessLocationId { get; set; }
        public WarehouseDTO Warehouse { get; set; }
        public List<object> Deliveries { get; set; }
        public List<PickupLogDTO> Log { get; set; }
        public bool IsRepeated { get; set; }
        public RepeatedDataDTO RepeatedData { get; set; }
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
