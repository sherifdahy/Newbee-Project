using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Response
{
    public class ShipmentDataDTO
    {
        public string _id { get; set; }
        public AddressDTO pickupAddress { get; set; }
        public AddressDTO dropOffAddress { get; set; }
        public AddressDTO returnAddress { get; set; }
        public string notes { get; set; }
        public decimal cod { get; set; }
        public StateDTO state { get; set; }
        public string maskedState { get; set; }
        public StateDTO statesData { get; set; }
        public ReceiverDTO receiver { get; set; }
        public TypeDTO type { get; set; }
        public string businessReference { get; set; }
        public string trackingNumber { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public SpecsDTO specs { get; set; }
        public HolderDTO holder { get; set; }
        public int deliveryAttemptsLength { get; set; }
        public int returnAttemptsLength { get; set; }
        public int pickupAttemptsLength { get; set; }
        public int outboundActionsCount { get; set; }
        public double shipmentFees { get; set; }
        public List<TimelineItemDTO> timeline { get; set; }
        public List<HistoryItemDTO> history { get; set; }
        public bool allowToOpenPackage { get; set; }
        public List<object> finalDeliveryGeoLocation { get; set; }
        public long creationTimestamp { get; set; }
        public int ticketCount { get; set; }
        public SenderDTO sender { get; set; }
        public AssignedHubDTO assignedHub { get; set; }
        public bool isConfirmedDelivery { get; set; }
        public int callsNumber { get; set; }
        public int smsNumber { get; set; }
        public string creationSrc { get; set; }
        public bool POSDelivery { get; set; }
        public string POSReceiptNo { get; set; }
        public bool isPudoOrder { get; set; }
        public int attemptsCount { get; set; }
        public InsurancePlanInfoDTO insurancePlanInfo { get; set; }
        public object state_before { get; set; }
        public WalletDTO wallet { get; set; }
        public List<LogDTO> log { get; set; }
        public List<object> attempts { get; set; }
    }

}
