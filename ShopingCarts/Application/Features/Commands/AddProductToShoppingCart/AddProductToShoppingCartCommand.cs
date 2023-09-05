using MediatR;

namespace ShopingCarts.Application.Features.Commands.AddProductToShoppingCart
{
    public class AddProductToShoppingCartCommand : IRequest<AddProductToShoppingCartCommandResult>
    {
        public int ShoppingCartId { get; set; }
        public AddProductToShoppingCartCommandExternal ExternalContract { get; set; }
    }
}
