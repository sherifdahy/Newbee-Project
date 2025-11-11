using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NOTE.Solutions.Entities.Abstractions.Consts;
using NOTE.Solutions.Entities.Entities.Identity;

namespace NOTE.Solutions.Entities.EntitiesConfiguration;
public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasData(new ApplicationRole()
        {
            Id = DefaultRoles.AdminRoleId,
            Name = DefaultRoles.Admin,
            IsDefault = false,
            NormalizedName = DefaultRoles.Admin.ToUpper(),
            ConcurrencyStamp = DefaultRoles.AdminRoleConcurrencyStamp,
        },
        new ApplicationRole()
        {
            Id = DefaultRoles.MemberRoleId,
            IsDefault = true,
            Name = DefaultRoles.Member,
            NormalizedName= DefaultRoles.Member.ToUpper(),
            ConcurrencyStamp= DefaultRoles.MemberRoleConcurrencyStamp
        },
        new ApplicationRole()
        {
            Id= DefaultRoles.SupportRoleId,
            Name = DefaultRoles.Support,
            IsDefault = false,
            NormalizedName = DefaultRoles.Support.ToUpper(),
            ConcurrencyStamp = DefaultRoles.SupportRoleConcurrencyStamp
        },
        new ApplicationRole()
        {
            Id = DefaultRoles.ManagerRoleId,
            Name = DefaultRoles.Manager,
            IsDefault = false,
            NormalizedName = DefaultRoles.Manager.ToUpper(),
            ConcurrencyStamp = DefaultRoles.ManagerRoleConcurrencyStamp
        },
        new ApplicationRole()
        {
            Id = DefaultRoles.EmployeeRoleId,
            Name = DefaultRoles.Employee,
            IsDefault = false,
            NormalizedName = DefaultRoles.Employee.ToUpper(),
            ConcurrencyStamp = DefaultRoles.EmployeeRoleConcurrencyStamp
        });
    }
}
