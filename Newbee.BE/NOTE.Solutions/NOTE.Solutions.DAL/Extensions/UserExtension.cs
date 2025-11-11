using System.Security.Claims;

namespace NOTE.Solutions.API.Extensions;

public static class UserExtension
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        int.TryParse(userId, out var id);

        return id;
    }
}
