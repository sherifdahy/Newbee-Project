using NOTE.Solutions.BLL.Abstractions.Consts;
using NOTE.Solutions.BLL.Contracts.Manager.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.Manager.Validations;

public class ManagerValidator : AbstractValidator<ManagerRequest>
{
    public ManagerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");

        RuleFor(x => x.IdentifierNumber).NotEmpty().WithMessage("Identifier number is required");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .Matches(@"^01[0-9]{9}$").WithMessage("Phone number must be a valid Egyptian number (11 digits starting with 01)");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .Matches(RegexPatterns.Password).WithMessage("Password should be at least 8 digits and must contain Lowercase , NonAlphanumeric and Uppercase");
    }
}
