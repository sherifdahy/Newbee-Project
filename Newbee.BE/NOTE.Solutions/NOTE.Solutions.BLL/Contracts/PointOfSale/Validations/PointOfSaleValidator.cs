using NOTE.Solutions.BLL.Contracts.POS.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.POS.Validations;

public class PointOfSaleValidator : AbstractValidator<PointOfSaleRequest>
{
    public PointOfSaleValidator()
    {
        RuleFor(x => x.POSSerial)
            .NotEmpty().WithMessage("POS Serial is required.")
            .MaximumLength(100).WithMessage("POS Serial must not exceed 100 characters.");

        RuleFor(x => x.ClientId)
            .NotEmpty().WithMessage("Client Id is required.")
            .MaximumLength(100).WithMessage("Client Id must not exceed 100 characters.");

        RuleFor(x => x.ClientSecret)
            .NotEmpty().WithMessage("Client Secret is required.")
            .MaximumLength(100).WithMessage("Client Secret must not exceed 100 characters.");
    }
}

