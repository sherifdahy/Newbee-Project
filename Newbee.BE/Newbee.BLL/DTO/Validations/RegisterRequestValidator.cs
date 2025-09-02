using Newbee.DAL.Abstractions.Const;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .Length(3, 100).WithMessage("First name must be between 3 and 100 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .Length(3, 100).WithMessage("Last name must be between 3 and 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Matches(RegexPatterns.Password)
            .WithMessage("Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");

        RuleFor(x => x.SSN)
            .NotEmpty().WithMessage("SSN is required.")
            .Length(10, 20).WithMessage("SSN must be between 10 and 20 characters.");

        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage("Company name is required.")
            .Length(3, 200).WithMessage("Company name must be between 3 and 200 characters.");

        RuleFor(x => x.CompanyAddress)
            .NotEmpty().WithMessage("Company address is required.")
            .Length(3, 300).WithMessage("Company address must be between 3 and 300 characters.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\d{10,15}$").WithMessage("Phone number must contain only digits and be between 10 and 15 digits long.");

        RuleFor(x => x.TaxNumber)
            .NotEmpty().WithMessage("Tax number is required.")
            .Length(10, 25).WithMessage("Tax number must be between 10 and 25 characters.");
    }
}


