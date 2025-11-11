using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NOTE.Solutions.Entities.Entities.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.EntitiesConfiguration;

public class ActiveCodeConfiguration : IEntityTypeConfiguration<ActiveCode>
{
    public void Configure(EntityTypeBuilder<ActiveCode> builder)
    {
        builder.HasKey(ac => ac.Id);
        builder.HasIndex(ac => ac.Code).IsUnique();
        builder.Property(ac => ac.Code).IsRequired().HasMaxLength(100);

       
    }
}
