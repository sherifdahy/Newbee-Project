using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.DTO.Auth.Requests
{
    public record ConfirmEmailRequest(
    string Email,
    string Code
);
}
