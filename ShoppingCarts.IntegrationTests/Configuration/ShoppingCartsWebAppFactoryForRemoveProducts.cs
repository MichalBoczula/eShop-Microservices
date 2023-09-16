using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopingCarts.Persistance.Context;

namespace ShoppingCarts.IntegrationTests.Configuration
{
    public class ShoppingCartsWebAppFactoryForRemoveProducts<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<ShoppingCartContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<ShoppingCartContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryAddProductTest");
            });

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())

            using (var appContext = scope.ServiceProvider.GetRequiredService<ShoppingCartContext>())
            {
                try
                {
                    appContext.Database.EnsureCreated();

                    appContext.ShoppingCarts.Add(new ShopingCarts.Domain.Entities.ShoppingCart()
                    {
                        Id = 2,
                        IntegrationId = new Guid(),
                        Total = 6000
                    });

                    appContext.ShoppingCartProducts.Add(new ShopingCarts.Domain.Entities.ShoppingCartProduct()
                    {
                        Id = 11,
                        Price = 3000,
                        Quantity = 2,
                        ShoppingCartId = 2
                    });

                    appContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            });
        }
    }
}