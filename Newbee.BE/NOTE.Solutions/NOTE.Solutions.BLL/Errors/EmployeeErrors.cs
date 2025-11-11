
namespace NOTE.Solutions.BLL.Errors;

public static class EmployeeErrors
{
    public static readonly Error NotFound
        = new Error("Employee.NotFound", "Employee is Not Exist.", StatusCodes.Status404NotFound);
    public static readonly Error InvalidId
        = new Error("Employee.InvalidId", "Invalid Id.", StatusCodes.Status400BadRequest);
    public static readonly Error EmailDuplicated =
            new("Employee.EmailDuplicated", "This Email is Already Exist.", StatusCodes.Status409Conflict);

    public static readonly Error IdentifierNumberDuplicated =
            new("Employee.IdentifierNumberDuplicated", "This IdentifierNumber is Already Exist.", StatusCodes.Status409Conflict);
}
