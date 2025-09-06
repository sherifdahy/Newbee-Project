using Paymob.API.DTO;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newbee.DAL.Data;
using System.Net.Http.Json;
using Newbee.Entities.Models;

namespace Paymob.API.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _cardIntegrationId;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;

        public PaymentService(HttpClient httpClient, IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _apiKey = _configuration["Paymob:ApiKey"];
            _cardIntegrationId = _configuration["Paymob:CardIntegrationId"];
            _dbContext = dbContext;
        }

        public async Task<string> CreatePaymobOrderAsync(OrderRequest request)
        {
            var authToken = await GetAuthTokenAsync();

            var paymobRequest = new
            {
                auth_token = authToken,
                delivery_needed = false,
                amount_cents = (int)(request.TotalAmount ), 
                currency = request.Currency ?? "EGP",
                merchant_order_id = request.Id.ToString() 
            };

            var response = await _httpClient.PostAsJsonAsync("https://accept.paymobsolutions.com/api/ecommerce/orders", paymobRequest);
            response.EnsureSuccessStatusCode();
            var paymobOrder = await response.Content.ReadFromJsonAsync<PaymobOrderResponse>();

            var order = await _dbContext.Orders.FindAsync(request.Id);
            if (order == null)
                throw new Exception($"Order with ID {request.Id} not found.");

            order.PaymobOrderId = paymobOrder.id.ToString();
            await _dbContext.SaveChangesAsync();

            return paymobOrder.id.ToString();
        }

        public async Task<string> InitiatePaymentAsync(PaymobRequest paymentRequest)
        {
            var authToken = await GetAuthTokenAsync();

            var order = await _dbContext.Orders.FindAsync(paymentRequest.OrderId);
            if (order == null)
                throw new Exception($"Order with ID {paymentRequest.OrderId} not found.");

            var request = new
            {
                auth_token = authToken,
                amount_cents = (int)(paymentRequest.AmountCents),
                expiration = 3600,
                order_id = order.PaymobOrderId,
                billing_data = new
                {
                    first_name = paymentRequest.BillingData?.FirstName ?? "NA",
                    last_name = paymentRequest.BillingData?.LastName ?? "NA",
                    email = paymentRequest.BillingData?.Email ?? "NA",
                    phone_number = paymentRequest.BillingData?.PhoneNumber ?? "NA",
                  
                },
                currency = paymentRequest.Currency ?? "EGP",
                integration_id = _cardIntegrationId 
            };

            var response = await _httpClient.PostAsJsonAsync("https://accept.paymobsolutions.com/api/acceptance/payment_keys", request);
            response.EnsureSuccessStatusCode();
            var paymentKeyResponse = await response.Content.ReadFromJsonAsync<PaymobPaymentKeyResponse>();

            return paymentKeyResponse.token; 
        }

        public async Task ProcessWebhookAsync(string webhookData)
        {
            var webhook = JsonSerializer.Deserialize<PaymobWebhookResponse>(webhookData);
            if (webhook?.type != "TRANSACTION" || webhook.obj?.payment_method != "CARD")
                return; 

            var transaction = new Transaction
            {
                OrderId = int.Parse(webhook.obj.order.merchant_order_id),
                PaymobTransactionId = webhook.obj.id.ToString(),
                Amount = webhook.obj.amount_cents / 100m,
                Status = webhook.obj.success ? "Success" : "Failed",
                PaymentMethod = "Card",
                CreatedAt = DateTime.UtcNow
            };

            var order = await _dbContext.Orders.FindAsync(transaction.OrderId);
            if (order == null)
                throw new Exception($"Order with ID {transaction.OrderId} not found.");

            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<string> GetAuthTokenAsync()
        {
            var request = new { api_key = _apiKey };
            var response = await _httpClient.PostAsJsonAsync("https://accept.paymobsolutions.com/api/auth/tokens", request);
            response.EnsureSuccessStatusCode();
            var authResponse = await response.Content.ReadFromJsonAsync<PaymobAuthResponse>();
            return authResponse.token;
        }
    }

    public class PaymobAuthResponse
    {
        public string token { get; set; }
    }

    public class PaymobOrderResponse
    {
        public int id { get; set; }
    }

    public class PaymobPaymentKeyResponse
    {
        public string token { get; set; }
    }

    public class PaymobWebhookResponse
    {
        public string type { get; set; }
        public PaymobWebhookObj obj { get; set; }
    }

    public class PaymobWebhookObj
    {
        public int id { get; set; }
        public decimal amount_cents { get; set; }
        public bool success { get; set; }
        public string payment_method { get; set; }
        public PaymobWebhookOrder order { get; set; }
    }

    public class PaymobWebhookOrder
    {
        public int id { get; set; }
        public string merchant_order_id { get; set; }
    }
}