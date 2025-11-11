using FluentValidation;
using NOTE.Solutions.BLL.Contracts.Tax.Requests;

namespace NOTE.Solutions.BLL.Contracts.Tax.Validations;

public class TaxValidator : AbstractValidator<TaxRequest>
{
    public TaxValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(50).WithMessage("Code must not exceed 50 characters.");
    }
}
