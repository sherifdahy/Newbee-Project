using System;

namespace Newbee.BLL.DTO.Company.Responses
{
    public class CompanyResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TaxRegistrationNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
