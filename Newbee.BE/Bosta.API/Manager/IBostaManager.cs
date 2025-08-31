using Bosta.API.Services.Lookup;
using Bosta.API.Services.PickupLocations;
using Bosta.API.Services.PickupRequests;
using Bosta.API.Services.Price;
using Bosta.API.Services.Shipping;
using System.Threading.Tasks;

namespace Bosta.API.Manager
{
    public interface IBostaManager
    {
        public IPickupRequestsService PickupRequestsService { get;}
        public IPickupLocationsService PickupLocationsService { get;}
        public IShippingService ShippingService { get;}
        public ILookupService LookupService { get;}
        public IPricingService PricingService { get;}
    }
}
