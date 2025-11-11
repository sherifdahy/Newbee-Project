
using NOTE.Solutions.BLL.Contracts.Employee.Validators;
using NOTE.Solutions.BLL.Contracts.POS.Validations;

namespace NOTE.Solutions.BLL.Contracts.Branch.Validations;

public class BranchValidator : AbstractValidator<BranchRequest> 
{
    public BranchValidator()
    {
        RuleFor(x => x.CityId)
            .NotEmpty().WithMessage("City is required.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Branch code is required.")
            .MaximumLength(50).WithMessage("Branch code must not exceed 50 characters.");

        RuleFor(x => x.BuildingNumber)
            .NotEmpty();

        RuleFor(x => x.Street)
            .NotEmpty();
    }
}
