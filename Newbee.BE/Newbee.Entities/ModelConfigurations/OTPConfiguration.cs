using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Newbee.Entities.ModelConfigurations;

public class OTPConfiguration:IEntityTypeConfiguration<OTP>   
{
    public void Configure(EntityTypeBuilder<OTP> builder)
    {
        builder.Property(x => x.Code)
            .IsRequired().HasMaxLength(6);
    }

    
}
