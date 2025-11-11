

namespace NOTE.Solutions.BLL.Contracts.Auth.Validations;

public class RegisterCompanyValidator : AbstractValidator<RegisterCompanyRequest>
{
    public RegisterCompanyValidator()
    {
        // Company name
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Company name is required.")
            .MaximumLength(200).WithMessage("Company name must not exceed 200 characters.");

        // RIN
        RuleFor(x => x.RIN)
            .NotEmpty().WithMessage("RIN is required.")
            .Matches(@"^\d{9}$").WithMessage("RIN must be numeric and 9 digits.");

        RuleFor(x => x.Manager)
            .NotNull().WithMessage("Manager information is required.")
            .SetValidator(new ManagerValidator());
    }
}
