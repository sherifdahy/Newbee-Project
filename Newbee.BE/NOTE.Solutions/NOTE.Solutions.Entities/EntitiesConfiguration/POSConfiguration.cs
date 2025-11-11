using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NOTE.Solutions.Entities.Entities.Company;

namespace NOTE.Solutions.Entities.EntitiesConfiguration;

public class POSConfiguration : IEntityTypeConfiguration<POS>
{
    public void Configure(EntityTypeBuilder<POS> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ClientId).IsRequired().HasMaxLength(100);
        builder.Property(p => p.ClientSecret).IsRequired().HasMaxLength(100);

        builder.HasIndex(x => new { x.POSSerial,x.BranchId }).IsUnique();
        
        builder.Property(p => p.POSSerial).IsRequired().HasMaxLength(100);
    }
}
