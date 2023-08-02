using MediatR;
using ShopingCarts.Application.Features.Commands.AddProductToShoppingCart;

namespace ShopingCarts.Application.Features.Commands.RemoveProductFromShoppingCart
{
    public class RemoveProductFromShoppingCartCommand : IRequest<RemoveProductFromShoppingCartCommandResult>
    {
        public RemoveProductFromShoppingCartCommandExternal ExternalContract { get; set; }
    }
}
