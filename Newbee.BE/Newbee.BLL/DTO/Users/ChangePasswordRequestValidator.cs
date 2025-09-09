using Newbee.DAL.Abstractions.Const;
using Newbee.Entities.Enums;

namespace Newbee.BLL.DTO.Users;
public  class ChangePasswordRequestValidator:AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x=>x.NewPassword)
            .NotEmpty()
              .Matches(RegexPatterns.Password)
            .WithMessage("Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");

    }
}
