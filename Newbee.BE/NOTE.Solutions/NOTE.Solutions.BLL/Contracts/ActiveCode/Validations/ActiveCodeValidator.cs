using NOTE.Solutions.BLL.Contracts.ActiveCode.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.ActiveCode.Validations;

public class ActiveCodeValidator : AbstractValidator<ActiveCodeRequest>
{
    public ActiveCodeValidator()
    {
        RuleFor(ac => ac.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(100).WithMessage("Code cannot exceed 100 characters.");
    }
}
