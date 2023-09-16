using MediatR;

namespace ShopingCarts.Application.Features.Commands.RemoveProductFromShoppingCart
{
    internal class RemoveProductFromShoppingCartCommand : IRequest<RemoveProductFromShoppingCartCommandResult>
    {
        public int ShoppingCartId { get; set; }
        public int ShoppingCartProductId { get; set; }
    }
}
