using Microsoft.EntityFrameworkCore;
using Newbee.Entities;
using Newbee.Entities.Models;
using Newbee.BLL.Interfaces;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Newbee.DAL.Data;

namespace Newbee.BLL.Services
{
    public class PaymobService : IPaymobService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public PaymobService(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<(Order Order, string RedirectUrl)> ProcessPaymentAsync(int orderId, string paymentMethod, decimal amount)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.ProductUnit)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");

            var customer = order.Customer;
            if (customer == null)
                throw new InvalidOperationException("Customer not found for this order.");

            // API Keys
            string apiKey = _configuration["Paymob:APIKey"] ??
           throw new ArgumentException("Paymob API key not configured");

            string secretKey = _configuration["Paymob:SecretKey"]
                ?? throw new ArgumentException("Paymob secret key not configured");
            string publicKey = _configuration["Paymob:PublicKey"]
                ?? throw new ArgumentException("Paymob public key not configured");

            // Generate unique reference
            int specialReference = RandomNumberGenerator.GetInt32(1000000, 9999999) + orderId;
            var amountCents = (int)(amount * 100);

            var billingData = new
            {
                first_name = customer.ApplicationUser.FirstName,
                last_name = customer.ApplicationUser.LastName,
                phone_number = customer.ApplicationUser.PhoneNumber,
                email = customer.ApplicationUser.Email,
                country = "EG", // You can map from customer if exists
                city = "N/A",
                street = "N/A",
                building = "N/A",
                apartment = "N/A",
                floor = "N/A"
            };

            var integrationId = int.Parse(DetermineIntegrationId(paymentMethod));

            var payload = new
            {
                amount = amountCents,
                currency = "EGP",
                payment_methods = new[] { integrationId },
                billing_data = billingData,
                items = order.OrderDetails.Select(od => new
                {
                    name = od.ProductUnit.Description,
                    amount = (int)(od.UnitPrice * od.Quantity * 100),
                    description = $"x{od.Quantity} {od.ProductUnit.Description}",
                    quantity = (int)od.Quantity
                }).ToArray(),
                customer = new
                {
                    firstname = billingData.first_name,
                    lastname = billingData.last_name,
                    Email = billingData.email,
                    extras = new { orderId = order.Id }
                },
                extras = new { orderId = order.Id, customerId = customer.Id },
                special_reference = specialReference,
                merchant_order_id = specialReference.ToString(),
                expiration = 3600
            };

            var httpClient = new HttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://accept.paymob.com/v1/intention/");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Token", secretKey);
            requestMessage.Content = JsonContent.Create(payload);

            var response = await httpClient.SendAsync(requestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Paymob Intention API failed: {response.StatusCode} - {responseContent}");

            var resultJson = JsonDocument.Parse(responseContent);
            var clientSecret = resultJson.RootElement.GetProperty("client_secret").GetString();

            var transaction = new Transaction
            {
                OrderId = order.Id,
                PaymobTransactionId = specialReference.ToString(),
                Amount = amount,
                Status = "Pending",
                PaymentMethod = paymentMethod,
                CreatedAt = DateTime.UtcNow,
                PaymobResponse = responseContent
            };

            _context.Transactions.Add(transaction);
            order.State = OrderState.Pending;
            await _context.SaveChangesAsync();

            string redirectUrl = $"https://accept.paymob.com/unifiedcheckout/?publicKey={publicKey}&clientSecret={clientSecret}";
            return (order, redirectUrl);
        }

        private string DetermineIntegrationId(string paymentMethod) =>
            paymentMethod?.ToLower() switch
            {
                "card" => _configuration["Paymob:CardIntegrationId"] ?? throw new ArgumentException("Card integration ID not configured"),
                "wallet" => _configuration["Paymob:MobileIntegrationId"] ?? throw new ArgumentException("Wallet integration ID not configured"),
                _ => throw new ArgumentException($"Invalid payment method: {paymentMethod}")
            };

        public async Task<Order> UpdateOrderSuccess(string specialReference)
        {
            var transaction = await _context.Transactions
                .Include(t => t.Order)
                .FirstOrDefaultAsync(t => t.PaymobTransactionId == specialReference);

            if (transaction == null)
                throw new KeyNotFoundException($"Transaction {specialReference} not found.");

            transaction.Status = "Success";
            transaction.Order.State = OrderState.Delivered;

            await _context.SaveChangesAsync();
            return transaction.Order;
        }

        public async Task<Order> UpdateOrderFailed(string specialReference)
        {
            var transaction = await _context.Transactions
                .Include(t => t.Order)
                .FirstOrDefaultAsync(t => t.PaymobTransactionId == specialReference);

            if (transaction == null)
                throw new KeyNotFoundException($"Transaction {specialReference} not found.");

            transaction.Status = "Failed";
            transaction.Order.State = OrderState.Canceled;

            await _context.SaveChangesAsync();
            return transaction.Order;
        }

        public string ComputeHmacSHA512(string data, string secret)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secret);
            var dataBytes = Encoding.UTF8.GetBytes(data);

            using var hmac = new HMACSHA512(keyBytes);
            var hash = hmac.ComputeHash(dataBytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
