using Integrations.ShoppingCart;
using MediatR;
using ShopingCarts.Application.Features.Commands.AddProductToShoppingCart;
using ShopingCarts.Domain.Entities;

namespace ShopingCarts.Application.Features.Commands.UpdateShoppingCart
{
    internal class UpdateShoppingCartCommand : IRequest<UpdateShoppingCartCommandResult>
    {
        public Guid ShoppingCartIntegrationId { get; set; }
        public List<ShoppingCartProductExternal> Products{ get; set; }
    }
}
