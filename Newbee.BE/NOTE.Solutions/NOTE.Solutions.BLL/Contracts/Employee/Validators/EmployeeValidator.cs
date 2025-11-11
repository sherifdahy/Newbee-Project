using NOTE.Solutions.BLL.Abstractions.Consts;
using NOTE.Solutions.BLL.Contracts.Employee.Requests;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.Employee.Validators;

public class EmployeeValidator : AbstractValidator<EmployeeRequest>
{
    public EmployeeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .Matches(@"^01[0-9]{9}$").WithMessage("Phone number must be a valid Egyptian number (11 digits starting with 01)");


        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.IdentifierNumber)
            .NotEmpty().WithMessage("Identifier Number is required.");

        RuleFor(x => x.Password)
           .NotEmpty().WithMessage("Password is required")
           .Matches(RegexPatterns.Password).WithMessage("Password should be at least 8 digits and must contain Lowercase , NonAlphanumeric and Uppercase");
    }
}
