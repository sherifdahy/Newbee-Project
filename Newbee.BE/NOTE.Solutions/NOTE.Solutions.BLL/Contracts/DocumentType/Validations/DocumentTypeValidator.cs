using FluentValidation;
using NOTE.Solutions.BLL.Contracts.DocumentType.Requests;

namespace NOTE.Solutions.BLL.Contracts.DocumentType.Validations;

public class DocumentTypeValidator : AbstractValidator<DocumentTypeRequest>
{
    public DocumentTypeValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Type is required.")
            .MaximumLength(100).WithMessage("Type must not exceed 100 characters.");
        RuleFor(x => x.Version)
            .NotEmpty().WithMessage("Version is required.")
            .MaximumLength(20).WithMessage("Version must not exceed 20 characters.");
    }
}
