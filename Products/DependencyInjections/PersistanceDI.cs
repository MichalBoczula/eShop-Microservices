using Microsoft.EntityFrameworkCore;
using Products.Application.Contracts.Persistance;
using Products.Persistance.Context;

namespace Products.DependencyInjections
{
    public static class PersistanceDI
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Products")));
            services.AddScoped<IProductsContext, ProductsContext>();
            return services;
        }
    }
}
