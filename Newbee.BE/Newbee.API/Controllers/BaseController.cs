using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Newbee.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    public int CustomerId
    {
        get
        {
            var claimValue = User.FindFirst("CustomerId")?.Value;
            return int.TryParse(claimValue, out var customerId) ? customerId : 0;
        }
    }
    public int CompanyId
    {
        get
        {
            var claimValue = User.FindFirst("CompanyId")?.Value;
            return int.TryParse(claimValue, out var companyId) ? companyId : 0;
        }
    }
    protected int UserId
    {
        get
        {
            var claimValue = User.FindFirst("CustomerId")?.Value;
            return int.TryParse(claimValue, out var userId) ? userId : 0;
        }
    }


}
