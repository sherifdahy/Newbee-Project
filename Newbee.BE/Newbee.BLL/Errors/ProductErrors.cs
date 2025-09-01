namespace Newbee.BLL.Errors;

public class ProductErrors
{
    public static readonly Error NotFound =
        new("Product.NotFound", "Product Not Found", StatusCodes.Status404NotFound);
    public static readonly Error InvalidId =
        new Error("Product.InvalidId", "Invalid Id.", StatusCodes.Status400BadRequest);
}
