using MediatR;

namespace ShopingCarts.Application.Features.Commands.RemoveProductFromShoppingCart
{
    internal class RemoveProductFromShoppingCartCommand : IRequest<RemoveProductFromShoppingCartCommandResult>
    {
        public RemoveProductFromShoppingCartCommandExternal ExternalContract { get; set; }
    }
}
