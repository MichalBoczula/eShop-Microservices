using ShopingCarts.Application.Features.Queries.GetShoppingCartById.Dtos;

namespace ShopingCarts.Application.Features.Commands.RemoveProductFromShoppingCart
{
    internal class RemoveProductFromShoppingCartCommandResult
    {
        public string? PositiveMessage { get; set; }
        public string? ErrorDescription { get; set; }
    }
}
