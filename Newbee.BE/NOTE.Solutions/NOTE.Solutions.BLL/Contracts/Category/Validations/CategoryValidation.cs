namespace NOTE.Solutions.BLL.Contracts.Category.Validations;

public class CategoryValidation:AbstractValidator<CategoryRequest>
{
    public CategoryValidation()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");
    }
}
