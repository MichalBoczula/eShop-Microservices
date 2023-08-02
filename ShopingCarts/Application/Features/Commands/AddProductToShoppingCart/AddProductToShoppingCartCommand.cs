using MediatR;

namespace ShopingCarts.Application.Features.Commands.AddProductToShoppingCart
{
    public class AddProductToShoppingCartCommand : IRequest<AddProductToShoppingCartCommandResult>
    {
        public AddProductToShoppingCartCommandExternal ExternalContract { get; set; }
    }
}
