namespace NOTE.Solutions.BLL.Errors;

public static class OrderErrors
{
    public static readonly Error InvalidId = new("Order.InvalidId", "The provided order ID is invalid.", 400);
    public static readonly Error NotFound = new("Order.NotFound", "The order was not found.", 404);
    public static readonly Error EmptyOrder = new("Order.EmptyOrder", "The order must contain at least one item.", 400);
    public static readonly Error InvalidStatus = new("Order.InvalidStatus", "The order status is invalid for this operation.", 400);
    public static readonly Error InsufficientStock = new("Order.InsufficientStock", "One or more products are out of stock.", 409);
    public static readonly Error CustomerRequired = new("Order.CustomerRequired", "An order must be associated with a customer.", 400);
    public static readonly Error UpdateFailed = new("Order.UpdateFailed", "Failed to update the order.", 500);
    public static readonly Error CancelFailed = new("Order.CancelFailed", "Unable to cancel the order.", 400);
}