using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Errors;
public class ProductCategoryErrors
{
    public static readonly Error NotFound
        = new Error("Category.NotFound", "Category is Not Exist.", StatusCodes.Status404NotFound);
    public static readonly Error InvalidId
        = new Error("Category.InvalidId", "Invalid Id.", StatusCodes.Status400BadRequest);

}
