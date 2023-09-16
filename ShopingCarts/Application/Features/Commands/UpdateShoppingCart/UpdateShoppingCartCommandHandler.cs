using AutoMapper;
using Integrations.ShoppingCart;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopingCarts.Application.Contracts;
using ShopingCarts.Application.Features.Commands.AddProductToShoppingCart;
using ShopingCarts.Application.Features.Common;
using ShopingCarts.Domain.Entities;

namespace ShopingCarts.Application.Features.Commands.UpdateShoppingCart
{
    internal class UpdateShoppingCartCommandHandler : CommandBase, IRequestHandler<UpdateShoppingCartCommand, UpdateShoppingCartCommandResult>
    {
        public UpdateShoppingCartCommandHandler(IShoppingCartContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<UpdateShoppingCartCommandResult> Handle(UpdateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var shoppingCart = await this._context.ShoppingCarts
                    .Where(x => x.IntegrationId == request.ShoppingCartIntegrationId)
                    .FirstOrDefaultAsync();

                if(shoppingCart == null)
                {
                    return new UpdateShoppingCartCommandResult
                    {
                        PositiveMessage = null,
                        ErrorDescription = $"Shopping cart identify by integrationId {request.ShoppingCartIntegrationId} doesn't exist"
                    };
                }
                
                var products = this._mapper.Map<List<ShoppingCartProduct>>(request.Products);
                shoppingCart.ShoppingCartProducts = products;

                await this._context.ShoppingCarts.AddAsync(shoppingCart);
                var result = this._context.SaveChangesAsync(cancellationToken);

                return new UpdateShoppingCartCommandResult
                {
                    PositiveMessage = $"Succesfully updated shopping cart identify by integrationId {shoppingCart.IntegrationId}",
                    ErrorDescription = null
                };
            }
            catch(Exception ex)
            {
                return new UpdateShoppingCartCommandResult
                {
                    PositiveMessage = null,
                    ErrorDescription = ex.Message
                };
            }
        }
    }
}
