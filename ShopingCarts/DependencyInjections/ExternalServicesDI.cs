using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Abstract;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Concrete;

namespace ShopingCarts.DependencyInjections
{
    internal static class ExternalServicesDI
    {
        internal static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            services.AddScoped<IProductHttpService, ProductHttpService>();
            return services;
        }
    }
}
