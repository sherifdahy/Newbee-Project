using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NOTE.Solutions.Entities.Entities.Address;

namespace NOTE.Solutions.Entities.EntitiesConfiguration;

public class GovernorateConfiguration : IEntityTypeConfiguration<Governorate>
{
    public void Configure(EntityTypeBuilder<Governorate> builder)
    {
        builder.HasKey(g => g.Id);

        builder.HasIndex(g => new { g.Name, g.CountryId }).IsUnique();
        builder.Property(g => g.Name).IsRequired().HasMaxLength(100);

        builder.HasIndex(g => g.Code).IsUnique();
        builder.Property(g => g.Code).IsRequired().HasMaxLength(50);


        builder.HasData(new Governorate()
        {
            Id = 1,
            Code = "Giza",
            Name = "Giza",
            CountryId = 1,
        });
    }
}
