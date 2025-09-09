using Paymob.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paymob.API.Services
{
    public interface IPaymentService
    {
        Task<string> CreatePaymobOrderAsync(OrderRequest request);
        Task<string> InitiatePaymentAsync(PaymobRequest paymentRequest);
        Task ProcessWebhookAsync(string webhookData);
    }
}
