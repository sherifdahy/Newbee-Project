using Microsoft.AspNetCore.Http;

namespace NOTE.Solutions.BLL.Errors;

public static class RoleErrors
{
    public static readonly Error NotFound
        = new Error("Role.NotFound", "Role is Not Exist.", StatusCodes.Status404NotFound);

    public static readonly Error InvalidPermissions
       = new Error("Role.InvalidPermissions", "Invalid Permissions.", StatusCodes.Status404NotFound);

    public static readonly Error InvalidId
        = new Error("Role.InvalidId", "Invalid Id.", StatusCodes.Status400BadRequest);
    public static readonly Error Duplicated =
            new("Role.DuplicatedRole", "Role is Already Exist.", StatusCodes.Status409Conflict);
}
