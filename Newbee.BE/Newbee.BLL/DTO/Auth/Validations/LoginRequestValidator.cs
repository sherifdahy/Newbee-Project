using Newbee.BLL.DTO.Auth.Requests;
using Newbee.DAL.Abstractions.Const;

namespace Newbee.BLL.DTO.Auth.Validations
{
    public class LoginRequestValidator:AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().Matches(RegexPatterns.Password);
               
        }
    }
}
