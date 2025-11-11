using NOTE.Solutions.BLL.Contracts.Unit.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.Unit.Validations;

public class UnitValidator : AbstractValidator<UnitRequest>
{
    public UnitValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required.");
    }
}
