using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ShopingCarts.Domain.Entities;

namespace ShopingCarts.Persistance.Configuration
{
    internal class ShoppingCartProductConfiguration : IEntityTypeConfiguration<ShoppingCartProduct>
    {
        void IEntityTypeConfiguration<ShoppingCartProduct>.Configure(EntityTypeBuilder<ShoppingCartProduct> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.ShoppingCartRef)
                .WithMany(x => x.ShoppingCartProducts)
                .HasForeignKey(x => x.ShoppingCartId);
            builder.Property(x => x.ShoppingCartId).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.ProductIntegrationId).IsRequired();
        }
    }
}
