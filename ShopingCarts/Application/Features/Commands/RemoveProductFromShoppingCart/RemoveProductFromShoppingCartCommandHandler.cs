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
                    .Where(x => x.Id == request.ShoppingCartId)
                    .Include(x => x.ShoppingCartProducts)
                    .FirstOrDefault();

                if (shoppingCart == null)
                {
                    return new RemoveProductFromShoppingCartCommandResult()
                    {
                        PositiveMessage = null,
                        ErrorDescription = $"Shopping cart identify by id {request.ShoppingCartId} doesn't exist"
                    };
                }

                var shoppingCartProductToRemove = shoppingCart.ShoppingCartProducts.FirstOrDefault(x => x.Id == request.ShoppingCartProductId);

                if (shoppingCartProductToRemove != null)
                {
                    if (shoppingCartProductToRemove.Quantity == 1)
                    {
                        shoppingCart.ShoppingCartProducts.Remove(shoppingCartProductToRemove);
                        this._context.ShoppingCarts.Update(shoppingCart);
                    }
                    else
                    {
                        shoppingCartProductToRemove.Quantity -= 1;
                        this._context.ShoppingCartProducts.Update(shoppingCartProductToRemove);
                    }

                    shoppingCart.Total = shoppingCart.ShoppingCartProducts.Aggregate(0, (total, prod) => total = prod.Price * prod.Quantity);

                    var result = await this._context.SaveChangesAsync(cancellationToken);

                    return new RemoveProductFromShoppingCartCommandResult()
                    {
                        PositiveMessage = $"Successfullyy removed with id {request.ShoppingCartProductId} product from shoppingCart identify by {request.ShoppingCartId}",
                        ErrorDescription = null
                    };
                        
                }
                else
                {
                    return new RemoveProductFromShoppingCartCommandResult()
                    {
                        PositiveMessage = null,
                        ErrorDescription = $"Product identify by id {request.ShoppingCartProductId} doesn't exist in shoppingCart identify by {request.ShoppingCartId}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new RemoveProductFromShoppingCartCommandResult()
                {
                    PositiveMessage = null,
                    ErrorDescription = $"Error wit message '{ex.Message}' occured during removing process for product idnetify by id {request.ShoppingCartProductId}"
                };
            }
        }
    }
}
