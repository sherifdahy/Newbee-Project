using NOTE.Solutions.BLL.Contracts.OrderLine.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.DocumentDetail.Validations;

public class OrderLineValidator : AbstractValidator<OrderLineRequest>
{
    public OrderLineValidator()
    {
        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("Unit Price must be greater than zero.");
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        RuleFor(x => x.ProductUnitId)
            .NotEmpty().WithMessage("Product Unit Id required.")
            .GreaterThan(0).WithMessage("Product Unit Id must be greater than zero.");
    }
}
