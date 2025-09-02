using Newbee.BLL.DTO.Product.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.DTO.Product.Validations;
public class CreateProductRequestValidator : AbstractValidator<ProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .Length(3, 100)
            .NotEmpty();


    }
}
