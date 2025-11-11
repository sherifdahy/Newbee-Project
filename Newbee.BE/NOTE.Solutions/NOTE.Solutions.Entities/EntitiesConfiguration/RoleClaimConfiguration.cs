using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NOTE.Solutions.Entities.Abstractions.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.EntitiesConfiguration;
public class RoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<int>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<int>> builder)
    {
        var permissions = Permissions.GetAllPermissions();
        
        var adminClaims = new List<IdentityRoleClaim<int>>();

        for (var i = 0; i < permissions.Count; i++)
        {
            adminClaims.Add(new IdentityRoleClaim<int>
            {
                Id = i + 1,
                ClaimType = Permissions.Type,
                ClaimValue = permissions[i],
                RoleId = DefaultRoles.AdminRoleId,
            });
        }


        builder.HasData(adminClaims);

    }
}
