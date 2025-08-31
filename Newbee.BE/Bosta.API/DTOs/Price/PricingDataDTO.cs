using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Price
{
    public class PricingDataDTO
    {
        public PricingTierDTO Tier { get; set; }
        public PackageSizeDTO Size { get; set; }
        public TransitCostDTO Transit { get; set; }
        public ServiceTypeDTO ServiceType { get; set; }
        public decimal ShippingFee { get; set; }
        public bool IsBostaMaterialFee { get; set; }
        public MaterialFeeDTO BostaMaterialFee { get; set; }
        public string Currency { get; set; }
        public decimal Vat { get; set; }
        public decimal PriceBeforeVat { get; set; }
        public decimal PriceAfterVat { get; set; }
        public decimal SizeEffectCost { get; set; }
        public decimal DropOffZoneFees { get; set; }
        public decimal PickupZoneFees { get; set; }
    }

    public class PricingTierDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public ZeroCodDiscountDTO ZeroCodDiscount { get; set; }
        public ExtraCodFeeDTO ExtraCodFee { get; set; }
        public ExpediteFeeDTO ExpediteFee { get; set; }
        public InsuranceFeeDTO InsuranceFee { get; set; }
        public CodFeeDTO CodFee { get; set; }
        public PosFeeDTO PosFee { get; set; }
        public MaterialFeeDTO BostaMaterialFee { get; set; }
        public TierConfigurationsDTO Configurations { get; set; }
        public ExtraWeightConfigDTO ExtraWeight { get; set; }
        public CountryInfoDTO Country { get; set; }
        public bool IsInitial { get; set; }
        public bool IsDefault { get; set; }
        public bool Deleted { get; set; }
        public OpeningPackageFeeDTO OpeningPackageFee { get; set; }
        public int V { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ZeroCodDiscountDTO
    {
        public decimal Amount { get; set; }
    }

    public class ExtraCodFeeDTO
    {
        public decimal Percentage { get; set; }
        public decimal CodAmount { get; set; }
        public decimal MinimumFeeAmount { get; set; }
    }

    public class ExpediteFeeDTO
    {
        public decimal Percentage { get; set; }
        public decimal MinimumFeeAmount { get; set; }
    }

    public class InsuranceFeeDTO
    {
        public decimal Percentage { get; set; }
        public decimal MinimumFeeAmount { get; set; }
    }

    public class CodFeeDTO
    {
        public decimal Amount { get; set; }
    }

    public class PosFeeDTO
    {
        public decimal Percentage { get; set; }
        public decimal MinimumFeeAmount { get; set; }
    }

    public class MaterialFeeDTO
    {
        public decimal Amount { get; set; }
    }

    public class TierConfigurationsDTO
    {
        public bool ZeroCodDiscount { get; set; }
        public bool ExtraCodFee { get; set; }
        public bool InsuranceFee { get; set; }
        public bool ExpediteFee { get; set; }
        public bool CodFee { get; set; }
        public bool PosFee { get; set; }
        public string PaymentFrequency { get; set; }
        public List<string> PaymentSchedule { get; set; }
        public List<string> PaymentTransferMethod { get; set; }
        public string Weighting { get; set; }
        public bool BostaMaterialFee { get; set; }
        public RestrictionDTO Restriction { get; set; }
        public bool OpeningPackageFee { get; set; }
    }

    public class RestrictionDTO
    {
        public bool MerchantsRestricted { get; set; }
        public List<string> BusinessIds { get; set; }
    }

    public class ExtraWeightConfigDTO
    {
        public decimal WeightThresholdInKg { get; set; }
        public decimal CostForWeightThreshold { get; set; }
        public decimal CostForAdditionalKgWeight { get; set; }
        public string Id { get; set; }
        public bool ExcludeDeliveryTypesFromAdditionalWeighCostEnabled { get; set; }
        public List<string> ExcludedDeliveryTypesFromAdditionalWeighCost { get; set; }
    }

    public class CountryInfoDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }
        public string Currency { get; set; }
        public decimal Vat { get; set; }
    }

    public class OpeningPackageFeeDTO
    {
        public decimal Amount { get; set; }
    }

    public class PackageSizeDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Rate { get; set; }
        public decimal Cost { get; set; }
        public decimal Multiplier { get; set; }
    }

    public class TransitCostDTO
    {
        public string Id { get; set; }
        public int OriginSectorId { get; set; }
        public int DestinationSectorId { get; set; }
        public decimal Cost { get; set; }
    }

    public class ServiceTypeDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Rate { get; set; }
        public decimal Cost { get; set; }
    }
}
