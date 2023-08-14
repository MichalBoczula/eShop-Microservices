using MediatR;
using ShopingCarts.Application.Features.Commands.Checkout;

namespace ShopingCarts.Application.Features.Commands.CreateShoppingCart
{
    internal class CheckoutCommand : IRequest<CheckoutCommandResult>
    {
        public CheckoutCommandExternal ExternalContract { get; set; }
    }
}
