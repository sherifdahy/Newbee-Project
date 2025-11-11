//using ETA.Consume.Contracts.Auth.Requests;
//using ETA.Consume.Contracts.BranchAddress.Requests;
//using ETA.Consume.Contracts.Item.Requests;
//using ETA.Consume.Contracts.Receipt.Requests;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;

//namespace NOTE.Solutions.API.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//public class ETAController(IETAService eTAService) : ControllerBase
//{
//    private readonly IETAService eTAService = eTAService;

//    [HttpGet]
//    public async Task<IActionResult> GetToken()
//    {
//        AuthPOSRequest request = new AuthPOSRequest
//        {
//            ClientId = "abfc4ab8-cefc-45ce-ac5d-bb93e1a3bf9e",
//            ClientSecret = "697a8a07-0913-423f-ade3-5ca35682b7ce",
//            POSSerial = "741"
//        };
//        //var result = await eTAService.GetTokenAsync(request);

//        return Ok();
//    }

//    [HttpPost("")]
//    public async Task<IActionResult> Submit()
//    {
//        AuthPOSRequest request = new AuthPOSRequest
//        {
//            ClientId = "abfc4ab8-cefc-45ce-ac5d-bb93e1a3bf9e",
//            ClientSecret = "697a8a07-0913-423f-ade3-5ca35682b7ce",
//            POSSerial = "741"
//        };

//        SubmitReceiptsRequest submitReceipts = new()
//        {
//            Receipts = new List<SubmitReceiptRequest>()
//            {
//                new SubmitReceiptRequest()
//                {
//                    Header = new ETA.Consume.Contracts.Header.ResRequestsponses.HeaderRequest()
//                    {
//                        DateTimeIssued = DateTime.UtcNow,
//                        Currency = "EGP",
//                        ExchangeRate = 0,
//                        ReceiptNumber = "1"
//                    },
//                    DocumentType = new ETA.Consume.Contracts.DocumentType.Requests.DocumentTypeRequest()
//                    {
//                        ReceiptType = "S",
//                        TypeVersion = "1.2"
//                    },
//                    Buyer = new ETA.Consume.Contracts.Buyer.Requests.BuyerRequest()
//                    {
//                        Type = "P",
//                    },
//                    Seller = new ETA.Consume.Contracts.Seller.Requests.SellerRequest()
//                    {
//                        DeviceSerialNumber = "741",
//                        ActivityCode = "8510",
//                        BranchCode  = "0",
//                        CompanyTradeName = "محمد ابراهيم مرسي خليل وشركاه",
//                        RIN = "310300657",
//                        BranchAddress = new BranchAddressRequest(){
//                            BuildingNumber = "0",
//                            Country = "EG",
//                            Governate = "Giza",
//                            Street = "sadasdas",
//                            RegionCity = "Imbaba"
//                        }
//                    },
//                    PaymentMethod = "C",
//                    ItemData = new List<ItemRequest>()
//                    {
//                        new ItemRequest()
//                        {
//                            InternalCode = "1",
//                            Description = "sadasdsad",
//                            ItemCode = "EG-310300657-A1",
//                            ItemType = "EGS",
//                            UnitType = "EA",
//                            Quantity = 1,
//                            NetSale = 1,
//                            TotalSale = 1,
//                            UnitPrice = 1,
//                            Total = 1,
//                        },
//                    },
//                    TotalSales = 1,
//                    NetAmount = 1,
//                    TotalAmount = 1,
//                }
//            }
//        };

//        return Ok();
//        //var result = await eTAService.Submit(submitReceipts, request);
//        //return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
//    }
//}
