using Bosta.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Newbee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IBostaManager _bostaManager;
        protected readonly string _apiKey;
        public BaseController(IBostaManager bostaManager)
        {
            _bostaManager = bostaManager;
            _apiKey = "f5fbc9a7a6cb7be437fa97b7b99d8938a4e2ac00a1b9714870b72e307bc42a9f";
        }
    }
}
