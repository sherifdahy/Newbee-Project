﻿using Newbee.DAL.Abstractions.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.DTO.Auth.Validations
{
    public class ResetPasswordValidator:AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Code)
              .NotEmpty();
            RuleFor(x => x.newPassword)
            .NotEmpty().Matches(RegexPatterns.Password)
            .WithMessage("Password must be at least 8 characters long and include: 1 uppercase, 1 lowercase, 1 number, and 1 special character.");
        }
    }
}
