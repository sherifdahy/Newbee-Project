namespace NOTE.Solutions.BLL.Errors;

public static class ProductErrors
{
    public static readonly Error Duplicated = new("Product.Duplicated", "The product already exists.", 400);
    public static readonly Error InvalidId = new("Product.InvalidId", "The provided product ID is invalid.", 400);
    public static readonly Error NotFound = new("Product.NotFound", "The product was not found.", 404);
}