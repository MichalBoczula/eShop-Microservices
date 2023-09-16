using AutoMapper;
using Integrations.Orders.Request;
using Integrations.Products.GetProductsByIntegrationId.Results;
using ShopingCarts.Application.Features.Commands.AddProductToShoppingCart;
using ShopingCarts.Domain.Entities;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AutoMapper.Tests")]
namespace ShopingCarts.Application.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingFromAssembly(Assembly.GetExecutingAssembly());
            this.CreateShoppingCartProductForAddProductToShoppingCartCommand();
            this.CreateShoppingCartExternalForCheckoutCommand();
        }

        private void ApplyMappingFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes().Where(
                    p => p.GetInterfaces().Any(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            types.ForEach(
                t =>
                    {
                        var instance = Activator.CreateInstance(t);
                        var method = t.GetMethod("Mapping");
                        method?.Invoke(instance, new object[] { this });
                    });
        }

        private void CreateShoppingCartProductForAddProductToShoppingCartCommand()
        {
            CreateMap<(int shoppingCartId, AddProductToShoppingCartCommandExternal ext, ProductExternal prod), ShoppingCartProduct>()
                           .ForMember(d => d.ShoppingCartId, opt => opt.MapFrom(s => s.shoppingCartId))
                           .ForMember(d => d.Quantity, opt => opt.MapFrom(s => s.ext.ShoppingCartProductQuantity))
                           .ForMember(d => d.ProductIntegrationId, opt => opt.MapFrom(s => s.ext.ShoppingCartProductIntegrationId))
                           .ForMember(d => d.Price, opt => opt.MapFrom(s => s.ext.ShoppingCartProductQuantity * s.prod.Price))
                           .ForMember(d => d.Id, opt => opt.Ignore())
                           .ForMember(d => d.ShoppingCartRef, opt => opt.Ignore());
        }

        private void CreateShoppingCartExternalForCheckoutCommand()
        {
            CreateMap<ShoppingCartProductExternal, ShoppingCartProduct>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.ShoppingCartId, opt => opt.Ignore())
                .ForMember(d => d.ShoppingCartRef, opt => opt.Ignore());

            CreateMap<ShoppingCart, ShoppingCartExternal>()
                .ForMember(d => d.ShoppingCartIntegrationId, opt => opt.MapFrom(s => s.IntegrationId))
                .ForMember(d => d.UserIntegrationId, opt => opt.MapFrom(s => s.UserRef.IntegrationId))
                .ForMember(d => d.Products, opt => opt.Ignore());
        }
    }
}
