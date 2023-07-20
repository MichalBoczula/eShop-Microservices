using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;

namespace Orders.Persistance.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IntegrationId).IsRequired();
            builder.HasIndex(x => x.IntegrationId).IsUnique();
        }
    }
}
