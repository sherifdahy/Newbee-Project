//using Microsoft.AspNetCore.Mvc;
//using Paymob.API.DTO;
//using Paymob.API.Services;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Configuration;

//namespace Newbee.API.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//public class PaymentsController : ControllerBase
//{
//    private readonly IPaymentService _paymentService;
//    private readonly IConfiguration _configuration;

//    public PaymentsController(IPaymentService paymentService, IConfiguration configuration)
//    {
//        _paymentService = paymentService;
//        _configuration = configuration;
//    }

//    [HttpPost("create-order")]
//    public async Task<IActionResult> CreateOrder([FromBody] OrderRequest orderRequest)
//    {
//        if (!ModelState.IsValid)
//            return BadRequest(ModelState);

//        try
//        {
//            var paymobOrderId = await _paymentService.CreatePaymobOrderAsync(orderRequest);
//            return Ok(new { PaymobOrderId = paymobOrderId });
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, new { Error = ex.Message });
//        }
//    }

//    [HttpPost("initiate-payment")]
//    public async Task<IActionResult> InitiatePayment([FromBody] PaymobRequest paymentRequest)
//    {
//        if (!ModelState.IsValid)
//            return BadRequest(ModelState);

//        try
//        {
//            var paymentKey = await _paymentService.InitiatePaymentAsync(paymentRequest);
//            var paymentUrl = $"https://accept.paymobsolutions.com/api/acceptance/iframes/{_configuration["Paymob:CardIntegrationId"]}?payment_token={paymentKey}";
//            return Ok(new { PaymentUrl = paymentUrl });
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, new { Error = ex.Message });
//        }
//    }

//    [HttpPost("webhook")]
//    public async Task<IActionResult> Webhook()
//    {
//        try
//        {
//            using var reader = new StreamReader(Request.Body);
//            var webhookData = await reader.ReadToEndAsync();
//            await _paymentService.ProcessWebhookAsync(webhookData);
//            return Ok();
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, new { Error = ex.Message });
//        }
//    }
//}