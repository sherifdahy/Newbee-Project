using Microsoft.EntityFrameworkCore;
using NOTE.Solutions.Entities.Entities.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.EntitiesConfiguration;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<City> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasIndex(c => new { c.Name, c.GovernorateId }).IsUnique();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        
        builder.HasIndex(c => c.Code).IsUnique();
        builder.Property(c => c.Code).IsRequired().HasMaxLength(50);

        builder.HasData(new City()
        {
            Id = 1,
            Code = "Imbaba",
            Name = "Imbaba",
            GovernorateId = 1
        });

        
    }
}
