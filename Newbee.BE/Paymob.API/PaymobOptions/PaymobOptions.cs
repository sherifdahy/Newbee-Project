

namespace Paymob.API.PaymobOptions
{
    public class PaymobOptions
    {
        public string ApiKey { get; set; } = string.Empty;          
        public int IntegrationId { get; set; }                    
        public int IframeId { get; set; }                       
        public string BaseUrl { get; set; } = "https://accept.paymob.com/api";  
        //public string CallbackUrl { get; set; } = string.Empty;     
    }
}