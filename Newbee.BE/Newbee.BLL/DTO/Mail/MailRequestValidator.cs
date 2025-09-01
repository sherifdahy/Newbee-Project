using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.DTO.Mail
{
    public class MailRequestValidator:AbstractValidator<MailRequest>
    {
        public MailRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Code).NotEmpty().Length(6);
        }
    }
}
