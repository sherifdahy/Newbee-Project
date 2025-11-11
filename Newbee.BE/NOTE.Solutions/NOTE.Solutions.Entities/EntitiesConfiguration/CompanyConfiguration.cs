using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NOTE.Solutions.Entities.Entities.Company;
using System.Reflection.Emit;

namespace NOTE.Solutions.Entities.EntitiesConfiguration;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {

        builder.HasKey(c => c.Id);
        builder.Property(c=>c.RIN).IsRequired().HasMaxLength(9);
        builder.HasIndex(c => c.RIN).IsUnique();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(200);

    }
}
