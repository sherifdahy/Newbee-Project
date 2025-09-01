using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities.Extensions;
public static class ModelBuilderExtensions
{
    public static void ApplyAllConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ModelBuilderExtensions).Assembly);
    }
}
