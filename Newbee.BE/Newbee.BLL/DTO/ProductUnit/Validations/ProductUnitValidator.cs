using Newbee.BLL.DTO.ProductUnit.Requests;

namespace Newbee.BLL.DTO.ProductUnit.Validations;
public class ProductUnitValidator : AbstractValidator<ProductUnitRequest>
{
    public ProductUnitValidator()
    {
        RuleFor(x => x.Description)
            .Length(3,200)
            .NotEmpty();

        RuleFor(x => x.Stock)
            .Must(x=> x >= 0)
            .NotEmpty();

        RuleFor(x => x.Price)
            .Must(x=>x >= 0)
            .NotEmpty();

        RuleFor(x => x.Rate)
            .Must(x => x >= 0 && x <= 5)
            .NotEmpty();

        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.UnitId)
            .NotEmpty();
        
        RuleFor(x => x.ProductColorId)
            .NotEmpty();

        RuleFor(x => x.ProductSizeId)
            .NotEmpty();

    }
}
