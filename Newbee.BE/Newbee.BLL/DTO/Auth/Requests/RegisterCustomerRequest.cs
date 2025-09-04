using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.DTO.Auth.Requests;

public record RegisterCustomerRequest
(
    string FirstName,
    string LastName,
    string SSN,
    string Email,
    string Password,
    string FirstLine,
    string PhoneNumber
);
