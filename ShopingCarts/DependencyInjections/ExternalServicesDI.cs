using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Concrete.Products;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Orders.Abstract;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Orders.Concrete;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Products.Abstract;

namespace ShopingCarts.DependencyInjections
{
    internal static class ExternalServicesDI
    {
        internal static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            services.AddScoped<IProductHttpService, ProductHttpService>();
            services.AddHttpClient<IProductHttpService, ProductHttpService>();
            services.AddScoped<IProductsHttpRequestHandler, ProductsHttpRequestHandler>();

            services.AddScoped<IOrderHttpService, OrderHttpService>();
            services.AddHttpClient<IOrderHttpService, OrderHttpService>();
        
            return services;
        }
    }
}
