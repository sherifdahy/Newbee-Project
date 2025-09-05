using FluentValidation;
using Newbee.BLL.DTO.Zone.Requests;

namespace Newbee.BLL.DTO.Zone.Validations
{
    public class ZoneValidations : AbstractValidator<ZoneRequest>
    {
        public ZoneValidations()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Zone name is required.")
                .Length(2, 100).WithMessage("Zone name must be between 2 and 100 characters.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Zone code is required.")
                .Length(2, 10).WithMessage("Zone code must be between 2 and 10 characters.");

            RuleFor(x => x.CityId)
                .GreaterThan(0).WithMessage("Valid CityId is required.");
        }
    }
}
