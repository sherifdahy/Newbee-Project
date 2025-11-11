using FluentValidation;
using NOTE.Solutions.BLL.Contracts.Role.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.Role.Validations;

public class RoleValidator : AbstractValidator<RoleRequest>
{
    public RoleValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Role Name is Required.").Length(3,256);
        RuleFor(x => x.Permissions).NotNull().NotEmpty();
        RuleFor(x => x.Permissions).Must(x => x.Distinct().Count() == x.Count).WithMessage("You cannot add DUPLICATED Permissions for the same Role")
            .When(x => x.Permissions != null);
    }
}
