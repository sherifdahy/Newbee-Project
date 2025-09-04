using System;

namespace Newbee.BLL.DTO.Company.Responses
{
    public class CompanyResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string TaxRegistrationNumber { get; set; } = string.Empty;
    }
}
