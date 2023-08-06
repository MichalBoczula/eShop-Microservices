using Microsoft.EntityFrameworkCore;
using Moq;
using Products.Persistance.Context;

namespace Products.Tests.Common
{
    internal static class ProductsManagerDbContextFactory
    {
        public static Mock<ProductsContext> Create()
        {
            var options = new DbContextOptionsBuilder<ProductsContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var mock = new Mock<ProductsContext>(options) { CallBase = true };
            var context = mock.Object;
            context.Database.EnsureCreated();
            context.SaveChanges();
            return mock;
        }

        public static void Destroy(ProductsContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
