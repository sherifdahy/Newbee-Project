namespace NOTE.Solutions.BLL.Errors;

public static class ProductUnitErrors
{
    public static readonly Error Duplicated = new("ProductUnit.Duplicated", "A product unit with the same Product and Unit already exists.", 400);
    public static readonly Error InvalidId = new("ProductUnit.InvalidId", "The provided product unit ID is invalid.", 400);
    public static readonly Error NotFound = new("ProductUnit.NotFound", "The product unit was not found.", 404);
    public static readonly Error InvalidDescription = new("ProductUnit.InvalidDescription", "Description is required and must not exceed 200 characters.", 400);
    public static readonly Error InvalidInternalCode = new("ProductUnit.InvalidInternalCode", "InternalCode is required and must not exceed 50 characters.", 400);
    public static readonly Error InvalidGlobalCode = new("ProductUnit.InvalidGlobalCode", "GlobalCode is required and must not exceed 50 characters.", 400);
    public static readonly Error InvalidUnitPrice = new("ProductUnit.InvalidUnitPrice", "UnitPrice must be greater than 0.", 400);
    public static readonly Error DuplicatedInternalCode = new("ProductUnit.DuplicatedInternalCode", "A product unit with the same Product and InternalCode already exists.", 400);
    public static readonly Error DuplicatedGlobalCode = new("ProductUnit.DuplicatedGlobalCode", "A product unit with the same GlobalCode and GlobalCodeType already exists.", 400);
}