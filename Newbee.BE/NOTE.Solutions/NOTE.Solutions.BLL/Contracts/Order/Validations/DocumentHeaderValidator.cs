using NOTE.Solutions.BLL.Contracts.Document.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.Document.Validations;

public class DocumentHeaderValidator : AbstractValidator<DocumentHeaderRequest>
{
    public DocumentHeaderValidator()
    {
        RuleFor(x => x.DateTime)
            .NotEmpty().WithMessage("Document date is required.");

        RuleFor(x => x.DateTime)
            .LessThanOrEqualTo(_ => DateTime.Now)
            .WithMessage("Document date cannot be in the future.");


        RuleFor(x => x.DocumentNumber)
         .NotEmpty().WithMessage("Document number is required.")
         .MaximumLength(50).WithMessage("Document number must not exceed 50 characters.")
         .Matches("^[0-9]+$").WithMessage("Document number must contain digits only.");

    }
}
