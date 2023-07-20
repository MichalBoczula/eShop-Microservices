using Microsoft.EntityFrameworkCore;
using Orders.Application.Contracts;
using Orders.Domain.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Orders.Persistance.Context
{
    internal class OrderContext : DbContext, IOrderContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<User> Users { get; set; }

        public OrderContext([NotNull] DbContextOptions<OrderContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.SeedShoppingCarts();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
