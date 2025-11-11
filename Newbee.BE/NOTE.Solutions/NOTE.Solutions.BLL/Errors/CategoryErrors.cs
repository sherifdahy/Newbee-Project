namespace NOTE.Solutions.BLL.Errors;

public static class CategoryErrors
{
    public static readonly Error NotFound
        = new Error("Category.NotFound", "Category is not exist.", StatusCodes.Status404NotFound);

    public static readonly Error InvalidId
        = new Error("Category.InvalidId", "Invalid Id.", StatusCodes.Status400BadRequest);

    public static readonly Error Duplicated
        = new Error("Category.Duplicated", "This category is already exist.", StatusCodes.Status409Conflict);
}
