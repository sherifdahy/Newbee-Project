

namespace Newbee.BLL.DTO.Users;

public class UpdateProfileRequestValidator:AbstractValidator<UpdateProfileRequest>
{
    public UpdateProfileRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .Length(3, 100);
        RuleFor(x => x.LastName)
            .NotEmpty()
            .Length(3, 100);
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Length(10, 15);
        RuleFor(x => x.FirstLine)
            .NotEmpty()
            .Length(5, 200);
    }
}
