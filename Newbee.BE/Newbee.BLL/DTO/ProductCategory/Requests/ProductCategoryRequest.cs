using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.DTO.ProductCategory.Requests;
public class ProductCategoryRequest
{
    public string Name { get; set; } = string.Empty;
    public int CompanyId { get; set; }

}
