using Api.Bosta.Services.Shipping;
using Bosta.API.Services.ApiCall;
using Bosta.API.Services.Lookup;
using Bosta.API.Services.PickupLocations;
using Bosta.API.Services.PickupRequests;
using Bosta.API.Services.Price;
using Bosta.API.Services.Shipping;

namespace Bosta.API.Manager
{
    public class BostaManager : IBostaManager
    {
        private readonly Lazy<PickupLocationsService> _pickupLocationsService;
        private readonly Lazy<PickupRequestsService> _pickupRequestsService;
        private readonly Lazy<ShippingService> _shippingService;
        private readonly Lazy<LookupService> _lookupService;
        private readonly Lazy<PricingService> _pricingService;  

        public BostaManager(IApiCall apiCall)
        {
            _pickupLocationsService = new Lazy<PickupLocationsService>(() => new PickupLocationsService(apiCall));
            _pickupRequestsService = new Lazy<PickupRequestsService>(() => new PickupRequestsService(apiCall));
            _shippingService = new Lazy<ShippingService>(() => new ShippingService(apiCall));
            _lookupService = new Lazy<LookupService>(() => new LookupService(apiCall));
            _pricingService = new Lazy<PricingService>(() => new PricingService(apiCall));
        }
        public IPickupRequestsService PickupRequestsService => _pickupRequestsService.Value;
        public IPickupLocationsService PickupLocationsService => _pickupLocationsService.Value;
        public IShippingService ShippingService => _shippingService.Value;
        public ILookupService LookupService => _lookupService.Value;
        public IPricingService PricingService => _pricingService.Value;
    }

}
