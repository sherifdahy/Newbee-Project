using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NOTE.Solutions.Entities.Entities.Company;
using System.Reflection.Emit;

namespace NOTE.Solutions.Entities.EntitiesConfiguration;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.Property(x => x.BuildingNumber).IsRequired();
        builder.Property(x => x.Street).IsRequired();
        builder.Property(x => x.CompanyId).IsRequired();
        builder.Property(x => x.CityId).IsRequired();
    }
}