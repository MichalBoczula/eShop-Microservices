using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopingCarts.Persistance.Context;

namespace ShoppingCarts.IntegrationTests.Configuration
{
    public class ShoppingCartsWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
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
                    options.UseInMemoryDatabase("InMemoryEmployeeTest");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())

                using (var appContext = scope.ServiceProvider.GetRequiredService<ShoppingCartContext>())
                {
                    try
                    {
                        appContext.Database.EnsureCreated();
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