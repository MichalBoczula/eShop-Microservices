using AutoMapper;
using Integrations.Products.Results;
using ShopingCarts.Application.Features.Queries.GetShoppingCartById.Dtos;
using ShopingCarts.Application.Mapping;
using ShopingCarts.Domain.Entities;

namespace ShopingCarts.Application.Features.Commands.AddProductToShoppingCart.Dtos
{
    public class ShoppingCartProductDto : IMapFrom<ShoppingCartProduct>
    {
        public Guid ProductIntegrationId { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        public int ShoppingCartId { get; set; }

        public void Mapping(Profile profile)
        {
           
            profile.CreateMap<ShoppingCartDto, ShoppingCart>();
        }
    }
}
