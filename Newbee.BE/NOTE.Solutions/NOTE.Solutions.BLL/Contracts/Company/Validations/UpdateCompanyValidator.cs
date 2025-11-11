using NOTE.Solutions.BLL.Contracts.ActiveCode.Validations;
using NOTE.Solutions.BLL.Contracts.Branch.Validations;
using NOTE.Solutions.BLL.Contracts.DocumentDetail.Validations;

namespace NOTE.Solutions.BLL.Contracts.Company.Validations;

public class UpdateCompanyValidator : AbstractValidator<UpdateCompanyRequest>
{
    public UpdateCompanyValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(3,100);
        RuleFor(x => x.RIN).NotEmpty().Must(x=>x.Length == 9);
    }
}
