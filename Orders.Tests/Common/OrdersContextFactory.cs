using Microsoft.EntityFrameworkCore;
using Moq;
using Orders.Persistance.Context;

namespace Orders.Tests.Common
{
    internal class OrdersContextFactory
    {
        public static Mock<OrderContext> Create()
        {
            var options = new DbContextOptionsBuilder<OrderContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var mock = new Mock<OrderContext>(options) { CallBase = true };
            var context = mock.Object;
            context.Database.EnsureCreated();
            context.SaveChanges();
            return mock;
        }

        public static void Destroy(OrderContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
