using Microsoft.EntityFrameworkCore;
using Moq;
using ShopingCarts.Persistance.Context;

namespace ShoppingCart.Tests.Common
{
    internal static class ShoppingCartContextFactory
    {
        public static Mock<ShoppingCartContext> Create()
        {
            var options = new DbContextOptionsBuilder<ShoppingCartContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var mock = new Mock<ShoppingCartContext>(options) { CallBase = true };
            var context = mock.Object;
            context.Database.EnsureCreated();
            context.SaveChanges();
            return mock;
        }

        public static void Destroy(ShoppingCartContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
