using AutoMapper;
using ShopingCarts.Application.Mapping;
using ShopingCarts.Domain.Entities;

namespace ShopingCarts.Application.Features.Queries.GetShoppingCartById.Dtos
{
    public class ShoppingCartProductDto : IMapFrom<ShoppingCartProduct>
    {
        public Guid ProductIntegrationId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShoppingCartProduct, ShoppingCartProductDto>();
        }
    }
}
