using Newbee.BLL.DTO.Company.Requests;

namespace Newbee.BLL.DTO.Company.Validations
{
    public class CreateCompanyRequestValidator : AbstractValidator<CompanyRequest>
    {
        public CreateCompanyRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Company name is required.")
                .Length(3, 100).WithMessage("Company name must be between 3 and 100 characters.");

            RuleFor(x => x.TaxRegistrationNumber)
                .NotEmpty().WithMessage("Tax registration number is required.")
                .Length(5, 50).WithMessage("Tax registration number must be between 5 and 50 characters.");
        }
    }
}
