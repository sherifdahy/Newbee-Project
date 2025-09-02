using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Newbee.Entities.ModelConfigurations;
public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
{
    public void Configure(EntityTypeBuilder<OrderDetails> builder)
    {
        builder.HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(od => od.ProductUnit)
            .WithMany(pu => pu.OrderDetails)
            .HasForeignKey(od => od.ProductUnitId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
