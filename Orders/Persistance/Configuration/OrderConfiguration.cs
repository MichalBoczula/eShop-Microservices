using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;

namespace Orders.Persistance.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        void IEntityTypeConfiguration<Order>.Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IntegrationId).IsRequired();
            builder.HasOne(x => x.UserRef)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId);
            builder.HasIndex(x => x.IntegrationId).IsUnique();
        }
    }
}
