using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NOTE.Solutions.Entities.Entities.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.EntitiesConfiguration;

public class ActiveCodeCompanyConfiguration : IEntityTypeConfiguration<ActiveCodeCompany>
{
    public void Configure(EntityTypeBuilder<ActiveCodeCompany> builder)
    {
        builder.HasKey(x => new { x.CompanyId, x.ActiveCodeId });
    }
}
