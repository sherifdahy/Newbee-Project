using Microsoft.AspNetCore.Authorization;
using NOTE.Solutions.Entities.Abstractions.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Authentication.Filters;
public class PermissionAuthorizationHandler : AuthorizationHandler<PermisssionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermisssionRequirement requirement)
    {
        var user = context.User.Identity;

        if (user is null || !user.IsAuthenticated)
            return;

        var hasPermission = context.User.Claims.Any(x => x.Value == requirement.permission && x.Type == Permissions.Type);

        if(hasPermission) context.Succeed(requirement);
    }

}
