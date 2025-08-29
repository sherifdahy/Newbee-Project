using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities.Models.ModelConfigurations;

public class OTPConfiguration:IEntityTypeConfiguration<OTP>   
{
    public void Configure(EntityTypeBuilder<OTP> builder)
    {
        builder.Property(x => x.Code)
            .IsRequired().HasMaxLength(6);
    }

    
}
