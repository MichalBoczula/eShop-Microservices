using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ShopingCarts.Domain.Entities;

namespace ShopingCarts.Persistance.Configuration
{
    internal class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        void IEntityTypeConfiguration<ShoppingCart>.Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.UserRef)
                .WithOne(x => x.ShoppingCartRef)
                .HasForeignKey<ShoppingCart>(x => x.UserId);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Total).IsRequired();
            builder.Property(x => x.IntegrationId).IsRequired();
            builder.HasIndex(x => x.IntegrationId).IsUnique();
        }
    }
}
