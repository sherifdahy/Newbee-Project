using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Newbee.Entities.ModelConfigurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.OwnsMany(x => x.RefreshTokens)
            .ToTable("RefreshTokens")
            .WithOwner
            ().HasForeignKey("UserId");
        builder.Property(x=>x.FirstName)
            .IsRequired()
                .HasMaxLength(100);
        builder.Property(x => x.LastName)
            .IsRequired()
                .HasMaxLength(100);
    }
}
