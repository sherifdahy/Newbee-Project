using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities.ModelConfigurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

            builder
                .HasMany(x=>x.ProductCategories)
                .WithOne(x=>x.Company)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
