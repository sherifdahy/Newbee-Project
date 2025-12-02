using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NOTE.Solutions.Entities.Abstractions.Consts;
using NOTE.Solutions.Entities.Entities.Identity;
namespace NOTE.Solutions.Entities.EntitiesConfiguration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasIndex(x => new { x.Email }).IsUnique();

        builder.OwnsMany(x => x.RefreshTokens).WithOwner();

        var passwordHasher = new PasswordHasher<ApplicationUser>();

        builder.HasData(new ApplicationUser()
        {
            Id = DefaultUsers.AdminId,
            UserName = DefaultUsers.AdminEmail,
            Email = DefaultUsers.AdminEmail,
            NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
            NormalizedUserName = DefaultUsers.AdminEmail.ToUpper(),
            ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp,
            SecurityStamp = DefaultUsers.AdminSecurityStamp,
            EmailConfirmed = true,
            PasswordHash = passwordHasher.HashPassword(null!, DefaultUsers.AdminPassword),
        },
        new ApplicationUser()
        {
            Id = DefaultUsers.ManagerId,
            UserName = DefaultUsers.ManagerEmail,
            Email = DefaultUsers.ManagerEmail,
            NormalizedEmail = DefaultUsers.ManagerEmail.ToUpper(),
            NormalizedUserName = DefaultUsers.ManagerEmail.ToUpper(),
            ConcurrencyStamp = DefaultUsers.ManagerConcurrencyStamp,
            SecurityStamp = DefaultUsers.ManagerSecurityStamp,
            EmailConfirmed = true,
            PasswordHash = passwordHasher.HashPassword(null!, DefaultUsers.ManagerPassword),
        },
        new ApplicationUser()        
        {
            Id = DefaultUsers.ClientId,
            UserName = DefaultUsers.ClientEmail,
            Email = DefaultUsers.ClientEmail,
            NormalizedEmail = DefaultUsers.ClientEmail.ToUpper(),
            NormalizedUserName = DefaultUsers.ClientEmail.ToUpper(),
            ConcurrencyStamp = DefaultUsers.ClientConcurrencyStamp,
            SecurityStamp = DefaultUsers.ClientSecurityStamp,
            EmailConfirmed = true,
            PasswordHash = passwordHasher.HashPassword(null!, DefaultUsers.ClientPassword),
        });
    }
}
