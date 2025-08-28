using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Response
{
    public class InsurancePlanInfoDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        public int rank { get; set; }
        public double orderValueFeePercentage { get; set; }
        public bool isInsuranceNotApplied { get; set; }
        public double orderMinimumFees { get; set; }
    }

}
