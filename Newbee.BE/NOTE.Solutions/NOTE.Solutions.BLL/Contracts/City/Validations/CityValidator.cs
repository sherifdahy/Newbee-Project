using NOTE.Solutions.BLL.Contracts.City.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.City.Validations;

public class CityValidator : AbstractValidator<CityRequest>
{
    public CityValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("City name is required.")
            .MaximumLength(100).WithMessage("City name must not exceed 100 characters.");
        RuleFor(c => c.Code)
            .NotEmpty().WithMessage("City code is required.")
            .Length(3,10).WithMessage("City code must be exactly 10 characters.");
        RuleFor(c => c.GovernorateId)
            .GreaterThan(0).WithMessage("GovernorateId must be a positive integer.");
    }
}
