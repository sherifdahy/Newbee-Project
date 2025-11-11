using NOTE.Solutions.BLL.Contracts.Branch.Validations;
using NOTE.Solutions.BLL.Contracts.Manager.Requests;
using NOTE.Solutions.Entities.Entities.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.Company.Requests;
public class UpdateCompanyRequest
{
    public string Name { get; set; } = string.Empty;
    public string RIN { get; set; } = string.Empty;
}
