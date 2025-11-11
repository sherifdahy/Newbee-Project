
namespace NOTE.Solutions.BLL.Errors;

public static class ActiveCodeErrors
{
    public static readonly Error NotFound
        = new Error("ActiveCode.NotFound", "ActiveCode is Not Exist.", StatusCodes.Status404NotFound);
    public static readonly Error InvalidId
        = new Error("ActiveCode.InvalidId", "Invalid Id.", StatusCodes.Status400BadRequest);
    public static readonly Error Duplicated =
            new("ActiveCode.Duplicated", "This ActiveCode is Already Exist.", StatusCodes.Status409Conflict);
}
