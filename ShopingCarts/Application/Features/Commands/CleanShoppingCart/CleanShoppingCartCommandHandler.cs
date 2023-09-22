using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using ShopingCarts.Application.Contracts;
using ShopingCarts.Application.Features.Commands.UpdateShoppingCart;
using ShopingCarts.Application.Features.Common;

namespace ShopingCarts.Application.Features.Commands.CleanShoppingCart
{
    internal class CleanShoppingCartCommandHandler : CommandBase, IRequestHandler<UpdateShoppingCartCommand, UpdateShoppingCartCommandResult>
    {
        public CleanShoppingCartCommandHandler(IShoppingCartContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<UpdateShoppingCartCommandResult> Handle(UpdateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var shoppingCart = await this._context.ShoppingCarts
                                       .Include(x => x.ShoppingCartProducts)
                                       .FirstOrDefaultAsync(
                                       x => x.IntegrationId == request.ShoppingCartIntegrationId,
                                       cancellationToken);

                if (shoppingCart == null)
                {
                    return new UpdateShoppingCartCommandResult()
                               {
                                   ErrorDescription =
                                       $"Shopping cart identify by integrationId: {request.ShoppingCartIntegrationId} doesn't exist.",
                                   PositiveMessage = null
                               };
                }

                shoppingCart.ShoppingCartProducts.ForEach(x => this._context.ShoppingCartProducts.Remove(x));

                await this._context.SaveChangesAsync(cancellationToken);

                return new UpdateShoppingCartCommandResult()
                           {
                               ErrorDescription =
                                   $"Shopping cart identify by integrationId: {request.ShoppingCartIntegrationId} has been cleaned up.",
                               PositiveMessage = null
                           };
            }
            catch (Exception e)
            {
                return new UpdateShoppingCartCommandResult()
                           {
                               ErrorDescription =
                                   $"There was error {e.Message}.",
                               PositiveMessage = null
                           };
            }
        }
    }
}
