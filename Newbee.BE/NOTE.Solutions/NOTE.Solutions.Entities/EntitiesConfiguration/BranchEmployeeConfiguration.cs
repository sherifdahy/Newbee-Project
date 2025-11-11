using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NOTE.Solutions.Entities.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.EntitiesConfiguration;

public class BranchEmployeeConfiguration : IEntityTypeConfiguration<BranchEmployee>
{
    public void Configure(EntityTypeBuilder<BranchEmployee> builder)
    {
        builder.HasKey(x=> new {x.BranchId,x.EmployeeId});
    }
}
