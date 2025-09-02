using Newbee.BLL.DTO.Platform.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.DTO.Platform.Validations;
public class PlatformValidator : AbstractValidator<PlatformRequest>
{
    public PlatformValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(3, 100);
    }
}
