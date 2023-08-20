using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Products.IntegrationTests.Configuration
{
    public class ProductsWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        //https://code-maze.com/aspnet-core-integration-testing/
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<Products.Persistance.Context.ProductsContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<Products.Persistance.Context.ProductsContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryEmployeeTest");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())

                using (var appContext = scope.ServiceProvider.GetRequiredService<Products.Persistance.Context.ProductsContext>())
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