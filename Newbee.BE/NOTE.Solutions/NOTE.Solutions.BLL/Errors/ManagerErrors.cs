
namespace NOTE.Solutions.BLL.Errors;

public static class ManagerErrors
{
    public static readonly Error NotFound
        = new Error("Manager.NotFound", "Manager is Not Exist.", StatusCodes.Status404NotFound);
    public static readonly Error InvalidId
        = new Error("Manager.InvalidId", "Invalid Id.", StatusCodes.Status400BadRequest);
    public static readonly Error EmailDuplicated =
            new("Manager.EmailDuplicated", "This Email is Already Exist.", StatusCodes.Status409Conflict);

    public static readonly Error IdentifierNumberDuplicated =
            new("Manager.IdentifierNumberDuplicated", "This IdentifierNumber is Already Exist.", StatusCodes.Status409Conflict);
}
