using NOTE.Solutions.BLL.Contracts.ProductUnit.Requests;

namespace NOTE.Solutions.BLL.Contracts.ProductUnit.Validations;

public class ProductUnitValidator : AbstractValidator<ProductUnitRequest>
{
    public ProductUnitValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");

        RuleFor(x => x.InternalCode)
            .NotEmpty().WithMessage("InternalCode is required.")
            .MaximumLength(50).WithMessage("InternalCode cannot exceed 50 characters.");

        RuleFor(x => x.GlobalCode)
            .NotEmpty().WithMessage("GlobalCode is required.")
            .MaximumLength(50).WithMessage("GlobalCode cannot exceed 50 characters.");

        RuleFor(x => x.GlobalCodeType)
            .IsInEnum().WithMessage("Invalid GlobalCodeType value.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("UnitPrice must be greater than 0.");

        RuleFor(x => x.UnitId)
            .GreaterThan(0).WithMessage("UnitId must be greater than 0.");
    }
}

