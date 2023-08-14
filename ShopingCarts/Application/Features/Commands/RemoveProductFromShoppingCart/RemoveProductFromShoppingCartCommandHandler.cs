using AutoMapper;

using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopingCarts.Application.Contracts;
using ShopingCarts.Application.Features.Common;

namespace ShopingCarts.Application.Features.Commands.RemoveProductFromShoppingCart
{
    internal class RemoveProductFromShoppingCartCommandHandler : CommandBase, IRequestHandler<RemoveProductFromShoppingCartCommand, RemoveProductFromShoppingCartCommandResult>
    {
        public RemoveProductFromShoppingCartCommandHandler(IShoppingCartContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<RemoveProductFromShoppingCartCommandResult> Handle(RemoveProductFromShoppingCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var shoppingCart = this._context.ShoppingCarts
                    .Where(x => x.Id == request.ExternalContract.ShoppingCartId)
                    .Include(x => x.ShoppingCartProducts)
                    .FirstOrDefault();

                if (shoppingCart == null)
                {
                    return new RemoveProductFromShoppingCartCommandResult()
                    {
                        PositiveMessage = null,
                        ErrorDescription = $"Shopping cart identify by id {request.ExternalContract.ShoppingCartId} doesn't exist"
                    };
                }

                var shoppingCartProductToRemove = shoppingCart.ShoppingCartProducts.FirstOrDefault(x => x.Id == request.ExternalContract.ShoppingCartProductId);

                if (shoppingCartProductToRemove != null)
                {
                    if (shoppingCartProductToRemove.Quantity <= request.ExternalContract.ShoppingCartProductQuantity)
                    {
                        shoppingCart.ShoppingCartProducts.Remove(shoppingCartProductToRemove);
                        this._context.ShoppingCarts.Update(shoppingCart);
                    }
                    else
                    {
                        shoppingCartProductToRemove.Quantity -= request.ExternalContract.ShoppingCartProductQuantity;
                        this._context.ShoppingCartProducts.Update(shoppingCartProductToRemove);
                    }

                    var result = await this._context.SaveChangesAsync(cancellationToken);


                    return new RemoveProductFromShoppingCartCommandResult()
                    {
                        PositiveMessage = $"Successfullyy removed with id {request.ExternalContract.ShoppingCartProductId} product from shoppingCart identify by {request.ExternalContract.ShoppingCartId}",
                        ErrorDescription = null
                    };
                        
                }
                else
                {
                    return new RemoveProductFromShoppingCartCommandResult()
                    {
                        PositiveMessage = null,
                        ErrorDescription = $"Product identify by id {request.ExternalContract.ShoppingCartProductId} doesn't exist in shoppingCart identify by {request.ExternalContract.ShoppingCartId}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new RemoveProductFromShoppingCartCommandResult()
                {
                    PositiveMessage = null,
                    ErrorDescription = $"Error wit message '{ex.Message}' occured during removing process for product idnetify by id {request.ExternalContract.ShoppingCartProductId}"
                };
            }
        }
    }
}
