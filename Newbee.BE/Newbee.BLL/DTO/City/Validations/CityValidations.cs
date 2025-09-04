using Newbee.BLL.DTO.City.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.DTO.City.Validations
{
    public class CityValidations : AbstractValidator<CityRequest>
    {
        public CityValidations()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("City name is required.")
                .Length(2, 100).WithMessage("City name must be between 2 and 100 characters.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("City code is required.")
                .Length(2, 10).WithMessage("City code must be between 2 and 10 characters.");

            RuleFor(x => x.CountryId)
                .GreaterThan(0).WithMessage("Valid CountryId is required.");
        }
    }
}
