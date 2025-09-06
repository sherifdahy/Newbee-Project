using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newbee.BLL.Interfaces;
using System.Security.Claims;
using System.Text;

namespace Newbee.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymobService _paymobService;
        private readonly IConfiguration _configuration;

        public PaymentController(IPaymobService paymobService, IConfiguration configuration)
        {
            _paymobService = paymobService;
            _configuration = configuration;
        }

        [Authorize]
        [HttpPost("create-payment-token")]
        public async Task<IActionResult> CreatePaymentToken([FromQuery] int orderId, [FromQuery] string paymentMethod)
        {
            if (orderId <= 0) return BadRequest("Invalid order ID.");
            if (string.IsNullOrWhiteSpace(paymentMethod)) return BadRequest("Payment method is required.");

            try
            {
                decimal totalAmount = 0; 

                var (order, redirectUrl) = await _paymobService.ProcessPaymentAsync(orderId, paymentMethod, totalAmount);
                return Ok(new { RedirectUrl = redirectUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("callback")]
        public IActionResult Callback()
        {
            var query = Request.Query;

            string[] fields = {
                "amount_cents","created_at","currency","error_occured","has_parent_transaction",
                "id","integration_id","is_3d_secure","is_auth","is_capture","is_refunded",
                "is_standalone_payment","is_voided","order","owner","pending",
                "source_data.pan","source_data.sub_type","source_data.type","success"
            };

            var concatenated = new StringBuilder();
            foreach (var field in fields)
            {
                if (query.TryGetValue(field, out var value))
                    concatenated.Append(value);
                else
                    return BadRequest($"Missing field: {field}");
            }

            string receivedHmac = query["hmac"];
            string calculatedHmac = _paymobService.ComputeHmacSHA512(concatenated.ToString(), _configuration["Paymob:HMAC"]);

            if (!receivedHmac.Equals(calculatedHmac, StringComparison.OrdinalIgnoreCase))
                return Unauthorized("Invalid HMAC");

            bool.TryParse(query["success"], out bool isSuccess);
            var merchantOrderId = query["merchant_order_id"];

            if (isSuccess)
                return Content("<h1>Payment Success ✅</h1>", "text/html");

            return Content("<h1>Payment Failed ❌</h1>", "text/html");
        }
    }
}
