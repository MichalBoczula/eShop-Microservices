using AutoMapper;
using Integrations.Orders.Request;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopingCarts.Application.Contracts;
using ShopingCarts.Application.Features.Commands.CreateShoppingCart;
using ShopingCarts.Application.Features.Common;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Orders.Abstract;

namespace ShopingCarts.Application.Features.Commands.Checkout
{
    internal class CheckoutCommandHandler : CommandBase, IRequestHandler<CheckoutCommand, CheckoutCommandResult>
    {
        private readonly IOrderHttpService _orderHttpService;

        public CheckoutCommandHandler(IShoppingCartContext context, IMapper mapper, IOrderHttpService orderHttpService) : base(context, mapper)
        {
            _orderHttpService = orderHttpService;
        }

        public async Task<CheckoutCommandResult> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var shoppingCart = await this._context.ShoppingCarts
                    .Include(x => x.UserRef)
                    .Include(x => x.ShoppingCartProducts)
                    .FirstOrDefaultAsync(x => x.Id == request.ExternalContract.ShoppingCartId);

                if (shoppingCart == null)
                {
                    return new CheckoutCommandResult()
                    {
                        PositiveMessage = null,
                        ErrorDescription = $"Shopping cart identify by id {request.ExternalContract.ShoppingCartId} doesn't exist"
                    };
                }

                var shoppingCartExternal = this._mapper.Map<ShoppingCartExternal>(shoppingCart);

                var products = this._mapper.Map< List<ShoppingCartProductExternal>>(shoppingCartExternal.Products);

                shoppingCartExternal.Products = products;

                var externalResponse = await this._orderHttpService.CreateOrder(shoppingCartExternal);

                if (externalResponse.PositiveMessage != null)
                {
                    this._context.ShoppingCarts.Remove(shoppingCart);
                    await this._context.SaveChangesAsync(cancellationToken);

                    return new CheckoutCommandResult()
                    {
                        PositiveMessage = externalResponse.PositiveMessage,
                        ErrorDescription = null
                    };
                }
                else
                {
                    return new CheckoutCommandResult()
                    {
                        PositiveMessage = null,
                        ErrorDescription = externalResponse.ErrorDescription
                    };
                }
            }
            catch (Exception ex)
            {
                return new CheckoutCommandResult()
                {
                    PositiveMessage = null,
                    ErrorDescription = $"Error with message {ex.Message} occured during creating order proccess for shopping cart identify by id {request.ExternalContract.ShoppingCartId}"
                };
            }
        }
    }
}
