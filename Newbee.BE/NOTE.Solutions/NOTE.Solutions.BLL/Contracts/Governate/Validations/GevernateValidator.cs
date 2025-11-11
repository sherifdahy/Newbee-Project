using NOTE.Solutions.BLL.Contracts.Governate.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.Governate.Validations;

public class GevernateValidator : AbstractValidator<GovernateRequest>
{
    public GevernateValidator()
    {
        RuleFor(g => g.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
        RuleFor(g => g.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(10).WithMessage("Code cannot exceed 10 characters.");
        RuleFor(g => g.CountryId)
            .GreaterThan(0).WithMessage("CountryId must be a positive integer.");
    }
}
