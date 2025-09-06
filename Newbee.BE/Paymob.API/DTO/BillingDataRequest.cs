using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paymob.API.DTO;

    public record BillingDataRequest
    (
       string FirstName ,
       string LastName ,
       string Email ,
       string PhoneNumber
    );