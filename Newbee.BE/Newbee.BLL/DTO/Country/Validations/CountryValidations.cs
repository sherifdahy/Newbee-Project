using Newbee.BLL.DTO.Company.Requests;
using Newbee.BLL.DTO.Country.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.DTO.Country.Validations
{
    public class CountryValidations : AbstractValidator<CountryRequest>
    {
        public CountryValidations()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Country name is required.")
                .Length(2, 100).WithMessage("Country name must be between 2 and 100 characters.");
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Country code is required.")
                .Length(2, 10).WithMessage("Country code must be between 2 and 10 characters.");
        }
    }
}
