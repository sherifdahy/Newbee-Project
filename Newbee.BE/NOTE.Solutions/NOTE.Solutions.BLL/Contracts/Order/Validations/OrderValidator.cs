using NOTE.Solutions.BLL.Contracts.Customer.Validators;
using NOTE.Solutions.BLL.Contracts.Document.Requests;
using NOTE.Solutions.BLL.Contracts.DocumentDetail.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.Order.Validations;

public class OrderValidator : AbstractValidator<OrderRequest>
{
    public OrderValidator()
    {
        RuleFor(x => x.PaymentMethod)
            .IsInEnum().WithMessage("Invalid payment method.");

        RuleFor(x => x.OrderLines)
            .NotEmpty().WithMessage("Order must have at least one line.");

        RuleFor(x => x.ActiveCodeId)
            .NotEmpty().WithMessage("Active Code required.");

        RuleFor(x => x.PosId)
            .NotEmpty().WithMessage("POS Id required.");


        RuleFor(x => x.Customer)
            .NotEmpty().SetValidator(new CustomerValidator());

        RuleForEach(x => x.OrderLines)
            .SetValidator(new OrderLineValidator());
    }
}
