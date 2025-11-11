using NOTE.Solutions.BLL.Contracts.Customer.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.Customer.Validators;

public class CustomerValidator : AbstractValidator<CustomerRequest>
{
    public CustomerValidator()
    {
        RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Customer name is required.")
                .MaximumLength(200).WithMessage("Customer name must not exceed 200 characters.");

        RuleFor(x => x.IdentificationNumber)
                .NotEmpty().WithMessage("IdentificationNumber is required.");

        RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid Customer type.");
    }
}
