using AutoMapper;
using ShopingCarts.Application.Mapping;
using ShopingCarts.Domain.Entities;

namespace ShopingCarts.Application.Features.Queries.GetShoppingCartById.Dtos
{
    public class ShoppingCartDto : IMapFrom<ShoppingCart>
    {
        public List<ShoppingCartProductDto> ShoppingCartProducts { get; set; }
        public int Total { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShoppingCart, ShoppingCartDto>();
        }
    }
}
