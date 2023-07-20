using Microsoft.EntityFrameworkCore;

using Orders.Application.Contracts;
using Orders.Persistance.Context;

namespace Orders.DependencyInjection
{
    internal static class PersistanceDI
    {
        internal static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Orders")));
            services.AddScoped<IOrderContext, OrderContext>();
            return services;
        }
    }
}
