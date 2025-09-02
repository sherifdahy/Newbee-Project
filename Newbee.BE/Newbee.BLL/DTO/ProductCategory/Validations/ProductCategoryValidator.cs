using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.DTO.ProductCategory.Validations;
public class ProductCategoryValidator : AbstractValidator<ProductCategoryRequest>
{
    public ProductCategoryValidator()
    {
        RuleFor(x => x.Name).Length(3,100).NotEmpty();
    }
}
