using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities.ModelConfigurations;
public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
    {
        builder.HasOne(x=>x.ShoppingCart)
            .WithMany(x=>x.ShoppingCartItems)
            .HasForeignKey(x=>x.ShoppingCartId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
