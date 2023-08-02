using ShopingCarts.Application.Features.Queries.GetShoppingCartById.Dtos;

namespace ShopingCarts.Application.Features.Queries.GetShoppingCartById
{
    internal class GetShoppingCartByIdQueryResult
    {
        public ShoppingCartDto? ShoppingCartDto { get; set; }

        public string? ErrorDescription { get; set; }
    }
}
