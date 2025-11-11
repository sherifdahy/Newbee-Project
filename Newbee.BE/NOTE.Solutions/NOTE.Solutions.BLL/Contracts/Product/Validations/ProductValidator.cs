using NOTE.Solutions.BLL.Contracts.Product.Requests;
using NOTE.Solutions.BLL.Contracts.ProductUnit.Validations;

namespace NOTE.Solutions.BLL.Contracts.Product.Validations;

public class ProductValidator : AbstractValidator<ProductRequest>
{
    public ProductValidator()
    {
        RuleFor(x=>x.Name).NotEmpty().Length(3,100);
    }
}
