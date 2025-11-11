using NOTE.Solutions.BLL.Contracts.Country.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.Country.Validations;

public class CountryValidator : AbstractValidator<CountryRequest>
{
    public CountryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Country name is required.")
            .MaximumLength(100).WithMessage("Country name must not exceed 100 characters.");
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Country code is required.")
            .Length(3, 10).WithMessage("Country code must be between 3 and 10 characters.");
    }
}
