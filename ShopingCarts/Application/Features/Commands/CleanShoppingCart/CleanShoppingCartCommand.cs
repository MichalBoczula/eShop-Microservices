using MediatR;
using ShopingCarts.Application.Features.Commands.UpdateShoppingCart;

namespace ShopingCarts.Application.Features.Commands.CleanShoppingCart
{
    internal class CleanShoppingCartCommand : IRequest<CleanShoppingCartCommandResult>
    {
        public Guid ShoppingCartIntegrationId { get; set; }
    }
}
