using Microsoft.EntityFrameworkCore;
using ShopingCarts.Application.Contracts;
using ShopingCarts.Persistance.Context;

namespace ShopingCarts.DependencyInjections
{
    internal static class PersistanceDI
    {
        internal static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShoppingCartContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ShoppingCart")));
            services.AddScoped<IShoppingCartContext, ShoppingCartContext>();
            return services;
        }
    }
}
