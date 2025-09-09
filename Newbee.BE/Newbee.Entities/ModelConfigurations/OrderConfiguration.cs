using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Newbee.Entities.ModelConfigurations;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
<<<<<<< HEAD
=======
        builder.Property(e => e.PaymobOrderId).HasMaxLength(150);
>>>>>>> d8396f1e8a19c352a3f4ce797f9913b100ef5c2b
    }
}
