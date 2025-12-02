namespace NOTE.Solutions.BLL.Errors;

public static class OrderLineErrors
{
    public static readonly Error InvalidId = new("OrderLine.InvalidId", "The provided order line ID is invalid.", 400);
    public static readonly Error NotFound = new("OrderLine.NotFound", "The order line was not found.", 404);
    public static readonly Error InvalidQuantity = new("OrderLine.InvalidQuantity", "The quantity must be greater than zero.", 400);
    public static readonly Error MaximumQuantityExceeded = new("OrderLine.MaximumQuantityExceeded", "The quantity exceeds the maximum allowed per item.", 400);
    public static readonly Error ProductRequired = new("OrderLine.ProductRequired", "Each order line must reference a product.", 400);
    public static readonly Error DuplicatedProduct = new("OrderLine.DuplicatedProduct", "This product already exists in the order.", 400);
    public static readonly Error PriceMismatch = new("OrderLine.PriceMismatch", "The product price has changed since adding to cart.", 409);
    public static readonly Error UnitPriceRequired = new("OrderLine.UnitPriceRequired", "Unit price is required for each item.", 400);
    public static readonly Error NegativePrice = new("OrderLine.NegativePrice", "Price cannot be negative.", 400);
    public static readonly Error UpdateFailed = new("OrderLine.UpdateFailed", "Failed to update the order line.", 500);
    public static readonly Error RemoveFailed = new("OrderLine.RemoveFailed", "Failed to remove the order line.", 500);
    public static readonly Error InvalidDiscount = new("OrderLine.InvalidDiscount", "Discount amount is invalid or exceeds the item price.", 400);
}