using FluentValidation;
using Newbee.API.Abstractions.Const;

namespace Newbee.API.DTO.Authentication
{
    public class RegisterRequestValidator:AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x=>x.FirstName)
                .Length(3,100)
                .NotEmpty();
            RuleFor(x => x.LastName)
                .Length(3, 100)
                .NotEmpty();
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();
            RuleFor(x => x.Password).Matches(RegexPatterns.Password)
                .NotEmpty()
                .WithMessage("Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
            RuleFor(x => x.CompanyName)
                .Length(3, 200)
                .NotEmpty();
            RuleFor(x => x.CompanyAddress)
                .Length(3, 300)
                .NotEmpty();
            RuleFor(x => x.PhoneNumber)
                .Length(10, 15)
                .NotEmpty();
            RuleFor(x => x.TaxNumber)
                .Length(10, 25)
                .NotEmpty();


        }
    }
}
