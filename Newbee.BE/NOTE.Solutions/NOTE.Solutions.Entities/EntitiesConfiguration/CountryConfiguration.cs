using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NOTE.Solutions.Entities.Entities.Address;
using NOTE.Solutions.Entities.Entities.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.EntitiesConfiguration;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(x=>x.Name).IsUnique();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

        builder.HasIndex(c => c.Code).IsUnique();
        builder.Property(c => c.Code).IsRequired().HasMaxLength(10);


        builder.HasData(new Country()
        {
            Id = 1,
            Code = "EGY",
            Name = "EGYPT"
        });

    }
}
