using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Authentication.Filters;
public class PermisssionRequirement : IAuthorizationRequirement
{
    public string permission { get; }
    public PermisssionRequirement(string permission)
    {
        this.permission = permission;
    }


}
