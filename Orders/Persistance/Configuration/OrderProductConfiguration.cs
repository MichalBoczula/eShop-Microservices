using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;

namespace Orders.Persistance.Configuration
{
    internal class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        void IEntityTypeConfiguration<OrderProduct>.Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProductIntegrationId).IsRequired();
            builder.HasIndex(x => x.ProductIntegrationId).IsUnique();
            builder.HasOne(x => x.OrderRef)
                .WithMany(x => x.OrderProducts)
                .HasForeignKey(x => x.OrderId);
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Price).IsRequired();
        }
    }
}